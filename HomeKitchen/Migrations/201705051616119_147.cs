namespace HomeKitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _147 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ingredients", "Permission", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ingredients", "Permission");
        }
    }
}
