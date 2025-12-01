using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Dynamic_Web_Site.Models.DataContext;
using Dynamic_Web_Site.Models.Model;

namespace Dynamic_Web_Site.Controllers
{
    public class AltKategoriController : Controller
    {
        private BKDBContext db = new BKDBContext();

        // GET: AltKategori
       
        public ActionResult Index()
        {
            var altKategori = db.AltKategori.Include(a => a.Kategori);
            return View(altKategori.ToList());
        }

        // GET: AltKategori/Details/5


        // GET: AltKategori/Create
       
        public ActionResult Create()
        {
            ViewBag.KTG_Id = new SelectList(db.Kategori, "KTG_Id", "KTG_Adi");
            return View();
        }

        // POST: AltKategori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create( AltKategori altKategori)
        {
            if (ModelState.IsValid)
            {
                altKategori.AKT_Create_Date = DateTime.Now;
                db.AltKategori.Add(altKategori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.KTG_Id = new SelectList(db.Kategori, "KTG_Id", "KTG_Adi", altKategori.KTG_Id);
            return View(altKategori);
        }

        // GET: AltKategori/Edit/5
        
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AltKategori altKategori = db.AltKategori.Find(id);
            if (altKategori == null)
            {
                return HttpNotFound();
            }
            ViewBag.KTG_Id = new SelectList(db.Kategori, "KTG_Id", "KTG_Adi", altKategori.KTG_Id);
            return View(altKategori);
        }

        // POST: AltKategori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id,AltKategori altKategori)
        {
            if (ModelState.IsValid)
            {
                var ak = db.AltKategori.Where(i => i.AKT_Id == altKategori.AKT_Id).SingleOrDefault();
                ak.AKT_Adi = altKategori.AKT_Adi;
                ak.AKT_Aciklama = altKategori.AKT_Aciklama;
                ak.KTG_Id = altKategori.KTG_Id;
                ak.AKT_Update_Date = DateTime.Now;
                ak.Status = altKategori.Status;
                if (ak.Status=="Close")
                {
                    ak.AKT_Delete_Date = DateTime.Now;
                }
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.KTG_Id = new SelectList(db.Kategori, "KTG_Id", "KTG_Adi", altKategori.KTG_Id);
            return View(altKategori);
        }

        // GET: AltKategori/Delete/5
       
        public ActionResult Delete(int? id)
        {
            var altKategori = db.AltKategori
                        .Include(a => a.Kategori) // Kategori'yi dahil ediyoruz
                        .FirstOrDefault(a => a.AKT_Id == id);
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
           
            if (altKategori == null)
            {
                return HttpNotFound();
            }
            return View(altKategori);
        }

        // POST: AltKategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AltKategori altKategori = db.AltKategori.Find(id);
            altKategori.Status = "Close";
            altKategori.AKT_Delete_Date = DateTime.Now;
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
