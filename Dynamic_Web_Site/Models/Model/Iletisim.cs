using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dynamic_Web_Site.Models.Model
{[Table("ILT_Iletisim")]
    public class Iletisim
    {
        [Key]
        public int ILT_Id { get; set; }
        [StringLength(250,ErrorMessage ="250 Karakter olmalıdır")]
        [DisplayName("İletişim Adres")]
        public string ILT_Adres { get; set; }
        [StringLength(250, ErrorMessage = "250 Karakter olmalıdır")]
        [DisplayName("İletişim Telefon")]
        public string ILT_Telefon { get; set; }

        [DisplayName("İletişim E-Posta")]
        public string ILT_Eposta { get; set; }

        [DisplayName("İletişim Fax")]
        public string ILT_Fax { get; set; }
        [DisplayName("Whatsapp")]
        public string ILT_Whatsapp { get; set; }
        [DisplayName("Facebook")]
        public string ILT_Facebook { get; set; }
        [DisplayName("Twitter")]
        public string ILT_Twitter { get; set; }
        [DisplayName("Instagram")]
        public string ILT_Instagram { get; set; }
        [DisplayName("İletişim Konum")]
        [StringLength(500)]
        public string ILT_Konum { get; set; }
    }
}