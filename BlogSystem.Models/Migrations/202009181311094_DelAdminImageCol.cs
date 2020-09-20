namespace BlogSystem.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DelAdminImageCol : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Admins", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Admins", "Image", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
