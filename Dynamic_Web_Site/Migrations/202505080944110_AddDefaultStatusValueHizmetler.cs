namespace Dynamic_Web_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefaultStatusValueHizmetler : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HZM_Hizmet", "Status", a => a.String(nullable: false, maxLength: 20, defaultValue: "Open"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HZM_Hizmet", "Status", a => a.String(nullable: false, maxLength: 20));
        }
    }
}
