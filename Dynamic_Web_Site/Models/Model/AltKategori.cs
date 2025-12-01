using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dynamic_Web_Site.Models.Model
{
    [Table("AKT_AltKategori")]
    public class AltKategori
    {
        [Key]
        public int AKT_Id { get; set; }
        [Required,StringLength(50,ErrorMessage ="50 Karakter olmalıdır")]
        [DisplayName("AltKategori Adı")]
        public string AKT_Adi { get; set; }
        [DisplayName("AltKategori Açıklama")]
        public string AKT_Aciklama { get; set; }


        [DisplayName("AltKategori Eklenme Tarihi")]
        public DateTime? AKT_Create_Date { get; set; }
        [DisplayName("AltKategori Güncelleme Tarihi")]
        public DateTime? AKT_Update_Date { get; set; }
        [DisplayName("AltKategori Kaldırılma Tarihi")]
        public DateTime? AKT_Delete_Date { get; set; }

        [DisplayName("AltKategori Durumu")]
        [Required, StringLength(20, ErrorMessage = "20 Karakter olmalıdır")]
        public string Status { get; set; }

        [DisplayName("Kategori Adı")]
        public int KTG_Id { get; set; }

       
        public virtual Kategori Kategori { get; set; }


        public ICollection<Urun> Uruns { get; set; }
    }
}