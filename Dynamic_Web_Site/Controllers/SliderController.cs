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
    public class SliderController : Controller
    {
        private BKDBContext db = new BKDBContext();

        // GET: Slider
        public ActionResult Index()
        {
            return View(db.Slider.ToList());
        }

        // GET: Slider/Details/5
       

        // GET: Slider/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Slider/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Slider slider, HttpPostedFileBase SLD_ResimURL)
        {
            if (ModelState.IsValid)
            {
                if (SLD_ResimURL != null)
                {


                    WebImage img = new WebImage(SLD_ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(SLD_ResimURL.FileName);

                    string ResimName = SLD_ResimURL.FileName + imginfo.Extension;

                    //img.Resize(300, 200);
                    img.Save("~/Uploads/Slider/" + ResimName);

                    slider.SLD_ResimURL = "/Uploads/Slider/" + ResimName;

                }
                slider.SLD_Create_Date = DateTime.Now;



                db.Slider.Add(slider);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slider);
        }

        // GET: Slider/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Slider/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,Slider slider, HttpPostedFileBase SLD_ResimURL)
        {
            if (ModelState.IsValid)
            {
                var k = db.Slider.Where(X => X.SLD_Id == slider.SLD_Id).SingleOrDefault();
                if (SLD_ResimURL != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(k.SLD_ResimURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(k.SLD_ResimURL));
                    }

                    WebImage img = new WebImage(SLD_ResimURL.InputStream);
                    FileInfo imginfo = new FileInfo(SLD_ResimURL.FileName);

                    string ResimName = SLD_ResimURL.FileName + imginfo.Extension;

                    //img.Resize(300, 200);
                    img.Save("~/Uploads/Slider/" + ResimName);

                    k.SLD_ResimURL = "/Uploads/Slider/" + ResimName;

                }
                k.SLD_Aciklama = slider.SLD_Aciklama;
                k.SLD_Baslik = slider.SLD_Baslik;
                k.SLD_Update_Date = slider.SLD_Update_Date;
                k.Status = slider.Status;

                if (k.Status == "Close")
                {
                    k.SLD_Delete_Date = DateTime.Now;
                }


               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slider);
        }

        // GET: Slider/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Slider slider = db.Slider.Find(id);
            if (slider == null)
            {
                return HttpNotFound();
            }
            return View(slider);
        }

        // POST: Slider/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Slider slider = db.Slider.Find(id);
            slider.Status = "Close";
            slider.SLD_Delete_Date = DateTime.Now;
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
