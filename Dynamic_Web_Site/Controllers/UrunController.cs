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
    public class UrunController : Controller
    {
        private BKDBContext db = new BKDBContext();

        // GET: Urun
        public ActionResult Index()
        {
            var urun = db.Urun.Include(u => u.AltKategori);
            return View(urun.ToList());
        }

        // GET: Urun/Details/5
       

        // GET: Urun/Create
        public ActionResult Create()
        {
            ViewBag.AKT_Id = new SelectList(db.AltKategori, "AKT_Id", "AKT_Adi");
            return View();
        }

        // POST: Urun/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Urun urun,HttpPostedFileBase URN_ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (URN_ResimURL != null)
                {


                    WebImage img = new WebImage(URN_ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(URN_ResimURL.FileName);

                    string ResimName = URN_ResimURL.FileName + imginfo.Extension;

                    
                    img.Save("~/Uploads/Urun/" + ResimName);

                    urun.URN_ResimURL = "/Uploads/Urun/" + ResimName;

                }
                urun.URN_Create_Date = DateTime.Now;




                db.Urun.Add(urun);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AKT_Id = new SelectList(db.AltKategori, "AKT_Id", "AKT_Adi", urun.AKT_Id);
            return View(urun);
        }

        // GET: Urun/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            ViewBag.AKT_Id = new SelectList(db.AltKategori, "AKT_Id", "AKT_Adi", urun.AKT_Id);
            return View(urun);
        }

        // POST: Urun/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Urun urun, HttpPostedFileBase URN_ResimURL)
        {
            if (ModelState.IsValid)
            {
                var k = db.Urun.Where(X => X.URN_Id == urun.URN_Id).SingleOrDefault();
                if (URN_ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(k.URN_ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(k.URN_ResimURL));
                    }

                    WebImage img = new WebImage(URN_ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(URN_ResimURL.FileName);

                    string ResimName = URN_ResimURL.FileName + imginfo.Extension;

                    img.Resize(300, 400);
                    img.Save("~/Uploads/Urun/" + ResimName);

                    k.URN_ResimURL = "/Uploads/Urun/" + ResimName;

                }
                k.URN_Update_Date = DateTime.Now;
                k.URN_Baslik = urun.URN_Baslik;
                k.URN_UrunAdi = urun.URN_UrunAdi;
                k.URN_UrunAciklama = urun.URN_UrunAciklama;
                k.Status = urun.Status;
                k.AKT_Id = urun.AKT_Id;



                if (k.Status=="Close")
                {
                    k.URN_Delete_Date = DateTime.Now;
                }
                
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AKT_Id = new SelectList(db.AltKategori, "AKT_Id", "AKT_Adi", urun.AKT_Id);
            return View(urun);
        }

        // GET: Urun/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urun urun = db.Urun.Find(id);
            if (urun == null)
            {
                return HttpNotFound();
            }
            return View(urun);
        }

        // POST: Urun/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Urun urun = db.Urun.Find(id);
            urun.Status = "Close";
            urun.URN_Delete_Date = DateTime.Now;
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
