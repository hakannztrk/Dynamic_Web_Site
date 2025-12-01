using Dynamic_Web_Site.Models.DataContext;
using Dynamic_Web_Site.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Helpers;

namespace Dynamic_Web_Site.Controllers
{
    public class HomeController : Controller
    {
        private BKDBContext db = new BKDBContext();
        [Route("")]
        [Route("Anasayfa")]
        public ActionResult Index()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View();
        }

        public ActionResult SliderPartial()
        {
            return View(db.Slider.ToList().OrderByDescending(x => x.SLD_Id));
        }

        [Route("Hizmetlerimiz")]
        public ActionResult Hizmetlerimiz()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hizmet.Where(x=>x.Status=="Open").ToList().OrderByDescending(x => x.HZM_Id));
        }
        [Route("Iletisims")]
        public ActionResult Iletisim()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            ViewBag.Uyari = TempData["Uyari"];
            return View(db.Iletisim.SingleOrDefault());
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Iletisims")]
        public ActionResult Iletisim(string name , string subject, string email , string message )
        {


            if (name != null)
            {//MAİLLL İLETİŞİMMMM
               

                // View'e dönebilirsin veya redirect
                ViewBag.Bildirim = "Mesajınız başarıyla gönderildi.";
                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "@gmail.com";
                WebMail.Password = ""; // Şifreyi ayarla
                WebMail.SmtpPort = 587;

                string body = $"Gönderen: {name}<br>Email: {email}<br><br>{message}";
                WebMail.Send("@gmail.com", subject, body, isBodyHtml: true);

                return RedirectToAction("Iletisim");
            }


            

            return View("Iletisim");
        }


        [Route("Hakkimizda")]
        public ActionResult Hakkimizda()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hakkimizda.FirstOrDefault());

        }

        public ActionResult KategoriPartial()
        {

            var kategoriler = db.Kategori.Include(k => k.AltKategoris).ToList();
            return View(kategoriler);

        }
       [Route("Urunler")]
        public ActionResult Urun(string arama)
        {
            var urunler = db.Urun.Include("AltKategori.Kategori").Where(u => u.Status == "Open");
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            if (!string.IsNullOrEmpty(arama))
            {
                urunler = urunler.Where(u =>
                    u.URN_UrunAdi.Contains(arama) ||
                    u.URN_Baslik.Contains(arama) ||
                    u.URN_UrunAciklama.Contains(arama)
                );
            }

            return View(urunler.ToList());
           
        }
        [Route("Urunler/{baslik}/{id:int}")]
        public ActionResult UrunDetay(int id)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var urun = db.Urun.Where(u => u.URN_Id == id).SingleOrDefault();

            return View(urun);
        }
       public ActionResult UrunPartial(int id, int urunId)
        {
            
            var urun = db.Urun.Where(u => u.AKT_Id == id && u.URN_Id!=urunId).ToList();
            return View(urun);
        }

       public ActionResult TanitimPartial()
        {

            var tanitim = db.Tanitim.Where(x=>x.Status=="Open").SingleOrDefault();

            return View(tanitim);

        }
        [Route("UrunKategori/{kategoriad}/{id:int}")]
        public ActionResult Kategori(int id)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var b = db.Urun.Include("AltKategori").OrderByDescending(x => x.URN_Id).Where(x => x.AltKategori.KTG_Id == id).ToList();
            return View(b);
            
        }
        [Route("UrunAltkategori/{altkategoriad}/{id:int}")]
        public ActionResult AltKategori(int id)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var b = db.Urun.Include("AltKategori").OrderByDescending(x => x.URN_Id).Where(x => x.AKT_Id == id).ToList();
            return View(b);
        }
        public ActionResult FooterPartial()
        {
            ViewBag.Iletisim = db.Iletisim.SingleOrDefault();
            ViewBag.Hizmet = db.Hizmet.ToList().OrderByDescending(x => x.HZM_Id);
            return PartialView();
        }
      
     

    }
}













