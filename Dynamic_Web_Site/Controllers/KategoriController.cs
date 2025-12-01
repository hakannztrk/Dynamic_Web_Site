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
    public class KategoriController : Controller
    {
        private BKDBContext db = new BKDBContext();

        // GET: Kategori
        public ActionResult Index()
        {
            return View(db.Kategori.ToList());
        }

        // GET: Kategori/Details/5
    

        // GET: Kategori/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Kategori/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                kategori.KTG_Create_Date = DateTime.Now;
                
                db.Kategori.Add(kategori);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(kategori);
        }

        // GET: Kategori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = db.Kategori.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // POST: Kategori/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Kategori kategori)
        {
            if (ModelState.IsValid)
            {
                var k = db.Kategori.Where(x => x.KTG_Id == kategori.KTG_Id).SingleOrDefault();
                k.KTG_Adi = kategori.KTG_Adi;
                k.KTG_Aciklama = kategori.KTG_Aciklama;
                k.KTG_Update_Date=DateTime.Now;
                k.Status = kategori.Status;
                if (k.Status=="Close")
                {
                    k.KTG_Delete_Date = DateTime.Now;
                }
               
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(kategori);
        }

        // GET: Kategori/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategori kategori = db.Kategori.Find(id);
            if (kategori == null)
            {
                return HttpNotFound();
            }
            return View(kategori);
        }

        // POST: Kategori/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Kategori kategori = db.Kategori.Find(id);
            kategori.Status = "Close";
            kategori.KTG_Delete_Date = DateTime.Now;
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
