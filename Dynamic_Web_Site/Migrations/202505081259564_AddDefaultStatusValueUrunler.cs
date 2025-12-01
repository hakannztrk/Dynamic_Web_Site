namespace Dynamic_Web_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefaultStatusValueUrunler : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.URN_Urun", "Status", d => d.String(nullable: false, maxLength: 20, defaultValue: "Open"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.URN_Urun", "Status", d => d.String(nullable: false, maxLength: 20));
        }
    }
}
