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
            {
                // Email gönderimi (E-posta ayarlarını buraya yazınız)
                // ViewBag.Bildirim = "Mesajınız başarıyla gönderildi.";
                // WebMail.SmtpServer = "smtp.gmail.com";
                // WebMail.EnableSsl = true;
                // WebMail.UserName = "your-email@gmail.com";
                // WebMail.Password = "your-password";
                // WebMail.SmtpPort = 587;
                // string body = $"Gönderen: {name}<br>Email: {email}<br><br>{message}";
                // WebMail.Send("your-email@gmail.com", subject, body, isBodyHtml: true);

                ViewBag.Bildirim = "Mesajınız başarıyla kaydedildi.";
                return RedirectToAction("Iletisim");
            }

            return View("Iletisim");
        }

        [Route("Hakkimizda")]
        public ActionResult Hakkimizda()
        {
            ViewBag.Kimlik = db.Kimlik.FirstOrDefault();
            var hakkimizda = db.Hakkimizda.FirstOrDefault();
            return View(hakkimizda ?? new Hakkimizda());
        }

        public ActionResult KategoriPartial()
        {
            var kategoriler = db.Kategori.Include(k => k.AltKategoris).ToList() ?? new List<Kategori>();
            return View(kategoriler);
        }

        [Route("Urunler")]
        public ActionResult Urun(string arama)
        {
            var urunler = db.Urun.Include("AltKategori.Kategori").Where(u => u.Status == "Open").ToList() ?? new List<Urun>();
            ViewBag.Kimlik = db.Kimlik.FirstOrDefault();
            
            if (!string.IsNullOrEmpty(arama))
            {
                urunler = urunler.Where(u =>
                    (u.URN_UrunAdi ?? "").Contains(arama) ||
                    (u.URN_Baslik ?? "").Contains(arama) ||
                    (u.URN_UrunAciklama ?? "").Contains(arama)
                ).ToList();
            }

            return View(urunler);
        }

        [Route("Urunler/{baslik}/{id:int}")]
        public ActionResult UrunDetay(int id)
        {
            ViewBag.Kimlik = db.Kimlik.FirstOrDefault();
            var urun = db.Urun.FirstOrDefault(u => u.URN_Id == id);

            if (urun == null)
                return HttpNotFound();

            return View(urun);
        }

        public ActionResult UrunPartial(int id, int urunId)
        {
            var urun = db.Urun.Where(u => u.AKT_Id == id && u.URN_Id != urunId).ToList() ?? new List<Urun>();
            return View(urun);
        }

        public ActionResult TanitimPartial()
        {
            var tanitim = db.Tanitim.FirstOrDefault(x => x.Status == "Open");
            return View(tanitim ?? new Tanitim());
        }

        [Route("UrunKategori/{kategoriad}/{id:int}")]
        public ActionResult Kategori(int id)
        {
            ViewBag.Kimlik = db.Kimlik.FirstOrDefault();
            var b = db.Urun.Include("AltKategori").Where(x => x.AltKategori.KTG_Id == id).OrderByDescending(x => x.URN_Id).ToList() ?? new List<Urun>();
            return View(b);
        }

        [Route("UrunAltkategori/{altkategoriad}/{id:int}")]
        public ActionResult AltKategori(int id)
        {
            ViewBag.Kimlik = db.Kimlik.FirstOrDefault();
            var b = db.Urun.Include("AltKategori").Where(x => x.AKT_Id == id).OrderByDescending(x => x.URN_Id).ToList() ?? new List<Urun>();
            return View(b);
        }

        public ActionResult FooterPartial()
        {
            ViewBag.Iletisim = db.Iletisim.FirstOrDefault();
            var hizmetler = db.Hizmet.ToList();
            ViewBag.Hizmet = hizmetler?.OrderByDescending(x => x.HZM_Id).ToList() ?? new List<Hizmet>();
            return PartialView();
        }
      
     

    }
}













