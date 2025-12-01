using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dynamic_Web_Site.Models.Model
{
    [Table("KTG_Kategori")]
    public class Kategori
    {
        [Key]
        public int KTG_Id { get; set; }
        [Required, StringLength(50, ErrorMessage = "50 Karakter olmalıdır")]
        [DisplayName("Kategori Adı")]
        public string KTG_Adi { get; set; }
        [DisplayName("Kategori Açıklama")]
        public string KTG_Aciklama { get; set; }

        [DisplayName("Kategori Eklenme Tarihi")]
        public DateTime? KTG_Create_Date { get; set; }
        [DisplayName("Kategori Güncelleme Tarihi")]
        public DateTime? KTG_Update_Date { get; set; }
        [DisplayName("Kategori Kaldırılma Tarihi")]
        public DateTime? KTG_Delete_Date { get; set; }

        [DisplayName("Kategori Durumu")]
        [Required, StringLength(20, ErrorMessage = "20 Karakter olmalıdır")]
        public string Status { get; set; }
       

        public virtual ICollection<AltKategori> AltKategoris { get; set; }
       
    }
}