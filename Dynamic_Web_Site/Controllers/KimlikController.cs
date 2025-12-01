using Dynamic_Web_Site.Models.DataContext;
using Dynamic_Web_Site.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Dynamic_Web_Site.Controllers
{
    public class KimlikController : Controller
    {
        BKDBContext db = new BKDBContext();

        // GET: Kimlik
        public ActionResult Index()
        {
            return View(db.Kimlik.ToList());

        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kimlik kimlik,HttpPostedFileBase KMK_LogoURL)
        {
            if (ModelState.IsValid)
            {
                if (KMK_LogoURL!=null)
                {
                    WebImage img = new WebImage(KMK_LogoURL.InputStream);
                    FileInfo imginfo = new FileInfo(KMK_LogoURL.FileName);
                    string ResimName = KMK_LogoURL.FileName + imginfo.Extension;
                    img.Save("~/Uploads/Kimlik/" + ResimName);

                    kimlik.KMK_LogoURL = "/Uploads/Kimlik/" + ResimName;

                }
                db.Kimlik.Add(kimlik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kimlik);
        }


        // GET: Kimlik/Edit/5
        public ActionResult Edit(int id)
        {
            var kimlik = db.Kimlik.Where(x => x.KMK_Id == id).SingleOrDefault();
            return View(kimlik);
        }

        // POST: Kimlik/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id, Kimlik kimlik, HttpPostedFileBase KMK_LogoURL)
        {
            if (ModelState.IsValid)
            {
                var k = db.Kimlik.Where(x => x.KMK_Id == id).SingleOrDefault();

                if (KMK_LogoURL != null)
                {
                    // Önceden var olan resmi sil
                    if (System.IO.File.Exists(Server.MapPath(k.KMK_LogoURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(k.KMK_LogoURL));
                    }

                    // Yeni resim işlemleri
                    WebImage img = new WebImage(KMK_LogoURL.InputStream);
                    FileInfo imginfo = new FileInfo(KMK_LogoURL.FileName);
                    //string logoname = Path.GetFileName(KMK_LogoURL.FileName);  // Dosya adını al
                    string logoname = KMK_LogoURL.FileName+imginfo.Extension;
                    // Resmi yeniden boyutlandır
                    img.Resize(300, 200);

                    // Yükleme yapılacak dizin
                    img.Save("~/Uploads/Kimlik/"+logoname);
                     // Klasörü oluştur
                   // string fullPath = Path.Combine(path, logoname);

                    // Görseli kaydet
                   // img.Save(fullPath);

                    // Veritabanındaki yolu güncelle
                    k.KMK_LogoURL = "/Uploads/Kimlik/" + logoname;
                }

                // Diğer alanları güncelle
                k.KMK_Title = kimlik.KMK_Title;
                k.KMK_Keywords = kimlik.KMK_Keywords;
                k.KMK_Description = kimlik.KMK_Description;
                k.KMK_Unvan = kimlik.KMK_Unvan;

                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kimlik);
        }




    }
}
