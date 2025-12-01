using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dynamic_Web_Site.Models.Model
{[Table("HZM_Hizmet")]
    public class Hizmet
    {
        [Key]
        public int HZM_Id { get; set; }
        [Required]
        [DisplayName("Hizmet Başlık")]
        [StringLength(50, ErrorMessage = "50 Karakter olmalıdır")]
        public string HZM_Baslik { get; set; }
        [DisplayName("Hizmet Açıklama")]
        [StringLength(500,ErrorMessage ="500 Karakter olmalıdır")]
        public string HZM_Aciklama { get; set; }
        [DisplayName("Hizmet Resim")]
        [StringLength(500, ErrorMessage = "500 Karakter olmalıdır")]
        public string HZM_ResimURL { get; set; }

        [DisplayName("Hizmet Eklenme Tarihi")]
        public DateTime? HZM_Create_Date { get; set; }
        [DisplayName("Hizmet Güncelleme Tarihi")]
        public DateTime? HZM_Update_Date { get; set; }
        [DisplayName("Hizmet Kaldırılma Tarihi")]
        public DateTime? HZM_Delete_Date { get; set; }


        [DisplayName("Hizmet Durumu")]
        [Required]
        public string Status { get; set; }
      
    }
}