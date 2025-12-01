using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dynamic_Web_Site.Models.Model
{
    [Table("ADM_Admin")]
    public class Admin
    {
        [Key]
        public int ADM_Id { get; set; }
        [Required,StringLength(50,ErrorMessage ="50 Karakter olmalıdır")]
        [DisplayName("Admin E-Posta")]
        public string ADM_Eposta { get; set; }
        [Required,StringLength(50,ErrorMessage ="50 Karakter olmalıdır")]
        [DisplayName("Admin Password")]
        public string ADM_Password { get; set; }
        [DisplayName("Admin Yetki")]
        [Required]
        public string ADM_Yetki { get; set; }
    }
}