namespace Dynamic_Web_Site.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDefaultStatusValueSliders : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SLD_Slider", "Status", a => a.String(nullable: false, maxLength: 20, defaultValue: "Open"));
        }

        public override void Down()
        {
            AlterColumn("dbo.SLD_Slider", "Status", a => a.String(nullable: false, maxLength: 20));
        }
    }
}
