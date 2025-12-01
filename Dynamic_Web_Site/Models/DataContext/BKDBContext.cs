using Dynamic_Web_Site.Models.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Dynamic_Web_Site.Models.DataContext
{
    public class BKDBContext:DbContext
    {
        public BKDBContext():base("SqlString")
        {
        }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<AltKategori> AltKategori { get; set; }
        public DbSet<Kategori> Kategori { get; set; }
        public DbSet<Hakkimizda> Hakkimizda { get; set; }
        public DbSet<Hizmet> Hizmet { get; set; }
        public DbSet<Iletisim> Iletisim { get; set; }
        public DbSet<Kimlik> Kimlik { get; set; }
        public DbSet<Urun> Urun { get; set; }
        public DbSet<Slider> Slider { get; set; }
        public DbSet<Tanitim> Tanitim { get; set; }


    }
}