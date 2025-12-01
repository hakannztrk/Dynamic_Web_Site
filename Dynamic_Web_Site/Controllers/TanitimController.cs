using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using Dynamic_Web_Site.Models.DataContext;
using Dynamic_Web_Site.Models.Model;

namespace Dynamic_Web_Site.Controllers
{
    public class TanitimController : Controller
    {
        private BKDBContext db = new BKDBContext();

        // GET: Tanitim
        public ActionResult Index()
        {
            return View(db.Tanitim.ToList());
        }

        // GET: Tanitim/Details/5


        // GET: Tanitim/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tanitim/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tanitim tanitim, HttpPostedFileBase TNT_Tanitim)
        {
            if (ModelState.IsValid)
            {
                // Video dosyasını kontrol et
                if (TNT_Tanitim != null && TNT_Tanitim.ContentLength > 0)
                {
                    // Dosyanın uzantısını al
                    var fileExtension = Path.GetExtension(TNT_Tanitim.FileName).ToLower();

                    // Yalnızca video dosyalarını kabul et
                    if (fileExtension == ".mp4" || fileExtension == ".mkv" || fileExtension == ".avi" || fileExtension == ".mov")
                    {
                        // Dosya adı oluşturma (dosya adı + uzantı)
                        var fileName = Path.GetFileName(TNT_Tanitim.FileName);

                        // Upload klasörüne kaydetme yolu
                        var filePath = Path.Combine(Server.MapPath("~/Uploads/Tanitim"), fileName);

                        // Videoyu "Tanitim" klasörüne kaydetme
                        TNT_Tanitim.SaveAs(filePath);

                        // Video URL'sini veritabanına kaydetme
                        tanitim.TNT_Tanitim = "/Uploads/Tanitim/" + fileName;
                    }
                    else
                    {
                        // Geçersiz dosya türü
                        ModelState.AddModelError("", "Lütfen geçerli bir video dosyası yükleyin.");
                        return View(tanitim);
                    }
                }

                // Tanıtımın diğer alanlarını güncelle
                tanitim.TNT_Create_Date = DateTime.Now;

                // Veritabanına kaydetme
                db.Tanitim.Add(tanitim);
                db.SaveChanges();

                // Kullanıcıyı index sayfasına yönlendir
                return RedirectToAction("Index");
            }

            return View(tanitim);
        }

        // GET: Tanitim/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tanitim tanitim = db.Tanitim.Find(id);
            if (tanitim == null)
            {
                return HttpNotFound();
            }
            return View(tanitim);
        }

        // POST: Tanitim/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Tanitim tanitim, HttpPostedFileBase TNT_Tanitim)
        {


            var k = db.Tanitim.Where(X => X.TNT_Id == id).SingleOrDefault();

            if (k != null)
            {
                // Eğer yeni bir video dosyası yüklenmişse
                if (TNT_Tanitim != null && TNT_Tanitim.ContentLength > 0)
                {
                    // Öncelikle eski videoyu silmeliyiz
                    if (!string.IsNullOrEmpty(k.TNT_Tanitim))
                    {
                        // Eski videonun dosya yolunu alıyoruz (sunucu üzerinde tam yol)
                        var oldFilePath = Server.MapPath(k.TNT_Tanitim);

                        // Eğer eski video dosyası varsa, silme işlemi yapıyoruz
                        if (System.IO.File.Exists(oldFilePath))
                        {
                            System.IO.File.Delete(oldFilePath);
                        }
                    }

                    // Dosyanın uzantısını al
                    var fileExtension = Path.GetExtension(TNT_Tanitim.FileName).ToLower();

                    // Yalnızca video dosyalarını kabul et
                    if (fileExtension == ".mp4" || fileExtension == ".mkv" || fileExtension == ".avi" || fileExtension == ".mov")
                    {
                        // Dosya adı oluşturma (dosya adı + uzantı)
                        var fileName = Path.GetFileName(TNT_Tanitim.FileName);

                        // Upload klasörüne kaydetme yolu
                        var filePath = Path.Combine(Server.MapPath("~/Uploads/Tanitim"), fileName);

                        // Videoyu "Tanitim" klasörüne kaydetme
                        TNT_Tanitim.SaveAs(filePath);

                        // Yeni video URL'sini veritabanına kaydetme
                        k.TNT_Tanitim = "/Uploads/Tanitim/" + fileName;
                    }
                    else
                    {
                        // Geçersiz dosya türü
                        ModelState.AddModelError("", "Lütfen geçerli bir video dosyası yükleyin.");
                        return View(tanitim);
                    }
                }

                // Tanıtımın diğer alanlarını güncelliyoruz
                k.TNT_TanıtımAdi = tanitim.TNT_TanıtımAdi;
                k.TNT_Update_Date = DateTime.Now;
                k.Status = tanitim.Status;

                if (k.Status == "Close")
                {
                    k.TNT_Delete_Date = DateTime.Now;
                }

                // Veritabanında değişiklikleri kaydediyoruz
                db.SaveChanges();
                return RedirectToAction("Index");
            }
           

            return View(tanitim);

        }
    
            


        // GET: Tanitim/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tanitim tanitim = db.Tanitim.Find(id);
            if (tanitim == null)
            {
                return HttpNotFound();
            }
            return View(tanitim);
        }

        // POST: Tanitim/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tanitim tanitim = db.Tanitim.Find(id);
            tanitim.Status = "Close";
            tanitim.TNT_Delete_Date = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
