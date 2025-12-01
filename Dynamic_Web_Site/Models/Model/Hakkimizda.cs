using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Dynamic_Web_Site.Models.Model
{   
    [Table("HKM_Hakkimizda")]
    public class Hakkimizda
    {
        [Key]
        public int HKM_Id { get; set; }

        [Required]
        [DisplayName("Hakkımızda Açıklama")]
        public string HKM_Aciklama { get; set; }
    }
}