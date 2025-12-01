using Dynamic_Web_Site.Models.DataContext;
using Dynamic_Web_Site.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Dynamic_Web_Site.Controllers
{
    public class HakkimizdaController : Controller
    {
        BKDBContext db = new BKDBContext();
        // GET: Hakkimizda
        [Route("Hakkimizda/Index")]
        public ActionResult Index()
        {
            var h = db.Hakkimizda.ToList();
            return View(h);
        }
        
        public ActionResult Edit(int id)
        {
            var h = db.Hakkimizda.Where(x => x.HKM_Id == id).FirstOrDefault();
            return View(h);
        }  
        public ActionResult Create()
        {
            return View();
        } 
        [HttpPost]
        public ActionResult Create(Hakkimizda hakkimizda)
        {
            if (ModelState.IsValid)
            {
                db.Hakkimizda.Add(hakkimizda);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hakkimizda);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id,Hakkimizda h)
        {
            if (ModelState.IsValid)
            {
                var hak = db.Hakkimizda.Where(x => x.HKM_Id == id).SingleOrDefault();

                hak.HKM_Aciklama = h.HKM_Aciklama;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            return View(h);
        }
    }
}