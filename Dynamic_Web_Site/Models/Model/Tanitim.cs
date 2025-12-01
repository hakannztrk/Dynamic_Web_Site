using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dynamic_Web_Site.Models.Model
{
    [Table("TNT_Tanitim")]
    public class Tanitim
    {
        [Key]
        public int TNT_Id { get; set; }
        [DisplayName("Site Tanıtım Adı")]
        public string  TNT_TanıtımAdi { get; set; }
        [DisplayName("Site TanıtımURL")]
        [StringLength(500, ErrorMessage = "500 Karakter olmalıdır")]
        public string TNT_Tanitim { get; set; }
        [DisplayName("Tanıtım Eklenme Tarihi")]
        public DateTime? TNT_Create_Date { get; set; }
        [DisplayName("Tanıtım Güncelleme Tarihi")]
        public DateTime? TNT_Update_Date { get; set; }
        [DisplayName("Tanıtım Kaldırılma Tarihi")]
        public DateTime? TNT_Delete_Date { get; set; }

        [DisplayName("Tanıtım Durumu")]
        [Required]
        public string Status { get; set; }
    }
}