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
    public class HizmetController : Controller
    {
        private BKDBContext db = new BKDBContext();

        // GET: Hizmet
      
        public ActionResult Index()
        {
            return View(db.Hizmet.ToList());
        }


       
        // GET: Hizmet/Create
        public ActionResult Create()
        {
          
            return View();
        }

        // POST: Hizmet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Hizmet hizmet, HttpPostedFileBase HZM_ResimURL)
        {
            
            if (ModelState.IsValid)
            {


            if (HZM_ResimURL != null)
            {


                WebImage img = new WebImage(HZM_ResimURL.InputStream);
                FileInfo imginfo = new FileInfo(HZM_ResimURL.FileName);

                string ResimName = HZM_ResimURL.FileName + imginfo.Extension;

                img.Resize(500, 400);
                img.Save("~/Uploads/Hizmet/" + ResimName);

                hizmet.HZM_ResimURL = "/Uploads/Hizmet/" + ResimName;

                }


                hizmet.HZM_Create_Date = DateTime.Now;

                db.Hizmet.Add(hizmet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hizmet);
        }
      
        // GET: Hizmet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hizmet hizmet = db.Hizmet.Find(id);
            if (hizmet == null)
            {
                return HttpNotFound();
            }
            return View(hizmet);
        }

        // POST: Hizmet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Hizmet hizmet, HttpPostedFileBase HZM_ResimURL)
        {
            if (ModelState.IsValid)
            {
                var k = db.Hizmet.Where(x => x.HZM_Id == hizmet.HZM_Id).SingleOrDefault();
                if (HZM_ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(k.HZM_ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(k.HZM_ResimURL));
                    }

                    WebImage img = new WebImage(HZM_ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(HZM_ResimURL.FileName);

                    string ResimName = HZM_ResimURL.FileName + imginfo.Extension;

                    img.Resize(300, 200);
                    img.Save("~/Uploads/Hizmet/" + ResimName);

                    k.HZM_ResimURL = "/Uploads/Hizmet/" + ResimName;

                }
                k.HZM_Baslik = hizmet.HZM_Baslik;
                k.HZM_Aciklama = hizmet.HZM_Aciklama;
                k.Status = hizmet.Status;
                k.HZM_Update_Date = DateTime.Now;
                if (k.Status=="Close")
                {
                    k.HZM_Delete_Date = DateTime.Now;
                }



                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hizmet);
        }

        // GET: Hizmet/Delete/5
    
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hizmet hizmet = db.Hizmet.Find(id);
            if (hizmet == null)
            {
                return HttpNotFound();
            }
            return View(hizmet);
        }

        // POST: Hizmet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Hizmet hizmet = db.Hizmet.Find(id);
            hizmet.Status = "Close";
            hizmet.HZM_Delete_Date = DateTime.Now;
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
