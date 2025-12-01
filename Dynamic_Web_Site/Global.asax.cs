using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity.Migrations;
using Dynamic_Web_Site.Migrations;
using System.Configuration;

namespace Dynamic_Web_Site
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Database Migration'ları opsiyonel / güvenli şekilde çalıştır
            bool runMigrations = false;
            bool.TryParse(ConfigurationManager.AppSettings["RunMigrationsOnStartUp"], out runMigrations);
            if (runMigrations)
            {
                try
                {
                    var migrator = new DbMigrator(new Dynamic_Web_Site.Migrations.Configuration());
                    migrator.Update();
                }
                catch (Exception ex)
                {
                    // Hata uygulamanın başlamasını engellememeli; Trace'e logla
                    System.Diagnostics.Trace.TraceError("Database migration failed on Application_Start: " + ex);
                }
            }

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
