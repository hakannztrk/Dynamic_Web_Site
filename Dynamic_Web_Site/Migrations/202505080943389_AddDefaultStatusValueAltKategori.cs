namespace Dynamic_Web_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefaultStatusValueAltKategori : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AKT_AltKategori", "Status", b => b.String(nullable: false, maxLength: 20, defaultValue: "Open"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AKT_AltKategori", "Status", b => b.String(nullable: false, maxLength: 20));
        }
    }
}
