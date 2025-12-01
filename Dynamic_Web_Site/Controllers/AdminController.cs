using Dynamic_Web_Site.Models.DataContext;
using Dynamic_Web_Site.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Dynamic_Web_Site.Controllers
{
    public class AdminController : Controller
    {
        BKDBContext db = new BKDBContext();
        // GET: Admin
        [Route("Yonetimpaneli")]
        public ActionResult Index()
        {
            ViewBag.UrunSay = db.Urun.Where(x=>x.Status=="Open").Count();
            ViewBag.KategoriSay = db.Kategori.Where(x => x.Status == "Open").Count();
            ViewBag.AltKategoriSay = db.AltKategori.Where(x => x.Status == "Open").Count();
            ViewBag.HizmetSay = db.Hizmet.Where(x => x.Status == "Open").Count();
            return View();
        }
        [Route("yonetim/giris")]
        public ActionResult Login()
        {
            if (!db.Admin.Any())
            {
                ViewBag.Uyari = "Sistem henüz kurulmamış. İlk admin hesabını oluşturunuz.";
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            if (admin == null || string.IsNullOrEmpty(admin.ADM_Eposta))
            {
                ViewBag.Uyari = "Kullanıcı Adı veya Şifre yanlış";
                return View(admin ?? new Admin());
            }

            var login = db.Admin.FirstOrDefault(x => x.ADM_Eposta == admin.ADM_Eposta);
            if (login == null)
            {
                ViewBag.Uyari = "Kullanıcı Adı veya Şifre yanlış";
                return View(admin);
            }

            if (login.ADM_Eposta == admin.ADM_Eposta && 
                login.ADM_Password == Crypto.Hash(admin.ADM_Password, "MD5"))
            {
                Session["adminid"] = login.ADM_Id;
                Session["eposta"] = login.ADM_Eposta;
                Session["yetki"] = login.ADM_Yetki;
                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Uyari = "Kullanıcı Adı veya Şifre yanlış";
            return View(admin);
        }
        [Route("yonetim/cikis")]
        public ActionResult Logout()
        {
            Session["adminid"] = null;
            Session["eposta"] = null;
            Session.Abandon();
            return RedirectToAction("Login","Admin");
            
        }

        public ActionResult AdminList()
        {
            if (Session["yetki"] == null)
            {
                return RedirectToAction("Login");
            }
            if (Session["yetki"].ToString() != "Admin")
            {
                int adminId = Convert.ToInt32(Session["adminid"]);
                var admin = db.Admin.Where(x => x.ADM_Id == adminId).SingleOrDefault();
                return RedirectToAction("Edit", new { id = admin.ADM_Id });

            }

            return View(db.Admin.ToList()) ;
        }
        public ActionResult Create()
        {
            if (Session["yetki"] == null)
            {
                return RedirectToAction("Login");
            }
            if (Session["yetki"].ToString() != "Admin")
            {
                int adminId = Convert.ToInt32(Session["adminid"]);
                var admin = db.Admin.Where(x => x.ADM_Id == adminId).SingleOrDefault();
                return RedirectToAction("Edit", new { id = admin.ADM_Id });
            }
                return View();
        }
        [HttpPost]
        public ActionResult Create(Admin admin ,string sifre,string eposta)
        {

            if (ModelState.IsValid)
            {
                admin.ADM_Password = Crypto.Hash(admin.ADM_Password,"MD5");
                db.Admin.Add(admin);
                db.SaveChanges();
                 return RedirectToAction("AdminList");
            }
            
            return View(admin);
        }
        public ActionResult EditForYetki(int id)
        {
            var admin = db.Admin.Where(x => x.ADM_Id == id).SingleOrDefault();
            return View(admin);
        }


        [HttpPost]
        public ActionResult EditForYetki(int id ,Admin admin)
        {
            if (ModelState.IsValid)
            {
                var a = db.Admin.Where(x => x.ADM_Id == id).SingleOrDefault();
                a.ADM_Eposta = admin.ADM_Eposta;
                a.ADM_Yetki = admin.ADM_Yetki;
                a.ADM_Password = admin.ADM_Password; 
                db.SaveChanges();
                return RedirectToAction("AdminList");
            }
            
            return View(admin);
        }






        public ActionResult Edit(int id)
        {
            var admin = db.Admin.Where(x => x.ADM_Id == id).SingleOrDefault();
            return View(admin);
        }
        [HttpPost]
        public ActionResult Edit(int id, Admin admin, string sifre, string eposta)
        {
            if (ModelState.IsValid)
            {
                var a = db.Admin.Where(x => x.ADM_Id == id).SingleOrDefault();
                a.ADM_Password = Crypto.Hash(admin.ADM_Password, "MD5");
                a.ADM_Eposta = admin.ADM_Eposta;
                a.ADM_Yetki = admin.ADM_Yetki; 
                db.SaveChanges();
                return RedirectToAction("AdminList");
            }
            return View(admin);
        }
        public ActionResult Delete(int id)
        {
            var admin = db.Admin.Where(x => x.ADM_Id == id).SingleOrDefault();
            return View(admin);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var a = db.Admin.Where(x => x.ADM_Id == id).SingleOrDefault();
            if (a != null)
            {
                db.Admin.Remove(a);
                db.SaveChanges();
                return RedirectToAction("AdminList");

            }
            return View();
        }
    }
}