using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dynamic_Web_Site.Models.Model
{
    [Table("KMK_Kimlik")]
    public class Kimlik
    {
        [Key]   
        public int KMK_Id { get; set; }
        [DisplayName("Site Başlık")]
        [Required,StringLength(100,ErrorMessage ="100 Karakter olmalıdır")]
        public string KMK_Title { get; set; }

        [DisplayName("Anahtar Kelimeler")]
        [Required, StringLength(200, ErrorMessage = "100 Karakter olmalıdır")]
        public string KMK_Keywords { get; set; }

        [DisplayName("Site Açıklama")]
        [Required, StringLength(300, ErrorMessage = "100 Karakter olmalıdır")]
        public string KMK_Description { get; set; }

        [DisplayName("Site Logo")]
        [StringLength(500, ErrorMessage = "500 Karakter olmalıdır")]
        public string KMK_LogoURL { get; set; }

        [DisplayName("Site Unvan")]
        
        public string KMK_Unvan { get; set; }

       
        //public  MyProperty { get; set; }

    }
}