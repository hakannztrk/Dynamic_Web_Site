using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dynamic_Web_Site.Models.Model
{
    [Table("SLD_Slider")]
    public class Slider
    {
        [Key]
        public int SLD_Id { get; set; }
        [Required, StringLength(150, ErrorMessage = "150 Karakter olmalıdır")]
        [DisplayName("Slider Başlık")]
        public string SLD_Baslik { get; set; }
        [StringLength(500, ErrorMessage = "500 Karakter olmalıdır")]
        [DisplayName("Slider Açıklama")]
        public string SLD_Aciklama { get; set; }
        [StringLength(500, ErrorMessage = "500 Karakter olmalıdır")]
        [DisplayName("Slider Resim")]
        public string  SLD_ResimURL { get; set; }
        [DisplayName("Slider Eklenme Tarihi")]
        public DateTime? SLD_Create_Date { get; set; }
        [DisplayName("Slider Güncelleme Tarihi")]
        public DateTime? SLD_Update_Date { get; set; }
        [DisplayName("Slider Kaldırılma Tarihi")]
        public DateTime? SLD_Delete_Date { get; set; }

        [DisplayName("Slider Durumu")]
        [Required]
        public string Status { get; set; }

    }
}
