using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dynamic_Web_Site.Models.Model
{
    [Table("URN_Urun")]
    public class Urun
    {
        [Key]
        public int URN_Id { get; set; }
        [Required, StringLength(100, ErrorMessage = "100 Karakter olmalıdır")]
        [DisplayName("Ürün Adı")]
        public string URN_UrunAdi { get; set; }
        [Required, StringLength(150, ErrorMessage = "150 Karakter olmalıdır")]
        [DisplayName("Ürün Başlık")]
        public string URN_Baslik { get; set; }
       
        [DisplayName("Ürün Açıklama")]
        public string URN_UrunAciklama { get; set; }
        [StringLength(500,ErrorMessage ="500 Karakter olmalıdır")]
        [DisplayName("Ürün Resim")]
        public string URN_ResimURL { get; set; }
        [DisplayName("Ürün Eklenme Tarihi")]
        public DateTime? URN_Create_Date { get; set; }
        [DisplayName("Ürün Güncelleme Tarihi")]
        public DateTime? URN_Update_Date { get; set; }
        [DisplayName("Ürün Kaldırılma Tarihi")]
        public DateTime? URN_Delete_Date { get; set; }

        [DisplayName("Ürün Durumu")]
        [Required]
        public string Status { get; set; }

        [DisplayName("Ürün AltKategori")]

        public int AKT_Id { get; set; }
        
        public AltKategori AltKategori { get; set; }

     
       
    }
}