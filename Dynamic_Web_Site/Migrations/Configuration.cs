namespace Dynamic_Web_Site.Migrations
{
    using Dynamic_Web_Site.Models.DataContext;
    using Dynamic_Web_Site.Models.Model;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Helpers;

    internal sealed class Configuration : DbMigrationsConfiguration<BKDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BKDBContext context)
        {
            var defaultAdmin = new Admin
            {
                ADM_Eposta = "admin@admin.com",
                ADM_Password = Crypto.Hash("admin123", "MD5"),
                ADM_Yetki = "Admin"
            };

            context.Admin.AddOrUpdate(a => a.ADM_Eposta, defaultAdmin);
            context.SaveChanges();
        }
    }
}
