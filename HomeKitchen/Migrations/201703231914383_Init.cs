namespace HomeKitchen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Categories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        ReceipeId = c.Int(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .ForeignKey("dbo.Recipes", t => t.ReceipeId)
                .Index(t => t.ReceipeId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Recipes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Preview = c.String(),
                        PhotoUrl = c.String(),
                        CookingTime = c.DateTime(nullable: false),
                        DateOfCreation = c.DateTime(nullable: false),
                        UserId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Steps",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                        PhotoUrl = c.String(),
                        ReceipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Recipes", t => t.ReceipeId)
                .Index(t => t.ReceipeId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Login = c.String(),
                        Password = c.String(),
                        Email = c.String(),
                        DateOfRegistration = c.DateTime(nullable: false),
                        Locked = c.Boolean(nullable: false),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Roles", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UserProfiles",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Surname = c.String(),
                        Name = c.String(),
                        Gender = c.String(),
                        PhotoUrl = c.String(),
                        DateOfBirth = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.FavouriteRecipes",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ReceipeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ReceipeId })
                .ForeignKey("dbo.Recipes", t => t.ReceipeId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ReceipeId);
            
            CreateTable(
                "dbo.Ingredients",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Measures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RecipeIngredients",
                c => new
                    {
                        IngredientId = c.Int(nullable: false),
                        ReceipeId = c.Int(nullable: false),
                        MeasureId = c.Int(nullable: false),
                        Amount = c.Double(nullable: false),
                    })
                .PrimaryKey(t => new { t.IngredientId, t.ReceipeId })
                .ForeignKey("dbo.Ingredients", t => t.IngredientId)
                .ForeignKey("dbo.Measures", t => t.MeasureId)
                .ForeignKey("dbo.Recipes", t => t.ReceipeId)
                .Index(t => t.IngredientId)
                .Index(t => t.ReceipeId)
                .Index(t => t.MeasureId);
            
            CreateTable(
                "dbo.RecipeRatings",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        ReceipeId = c.Int(nullable: false),
                        Rating = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.UserId, t.ReceipeId })
                .ForeignKey("dbo.Recipes", t => t.ReceipeId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.ReceipeId);
            
            CreateTable(
                "dbo.TagRecipes",
                c => new
                    {
                        RecipeId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.RecipeId, t.TagId })
                .ForeignKey("dbo.Recipes", t => t.RecipeId)
                .ForeignKey("dbo.Tags", t => t.TagId)
                .Index(t => t.RecipeId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TagRecipes", "TagId", "dbo.Tags");
            DropForeignKey("dbo.TagRecipes", "RecipeId", "dbo.Recipes");
            DropForeignKey("dbo.RecipeRatings", "UserId", "dbo.Users");
            DropForeignKey("dbo.RecipeRatings", "ReceipeId", "dbo.Recipes");
            DropForeignKey("dbo.RecipeIngredients", "ReceipeId", "dbo.Recipes");
            DropForeignKey("dbo.RecipeIngredients", "MeasureId", "dbo.Measures");
            DropForeignKey("dbo.RecipeIngredients", "IngredientId", "dbo.Ingredients");
            DropForeignKey("dbo.FavouriteRecipes", "UserId", "dbo.Users");
            DropForeignKey("dbo.FavouriteRecipes", "ReceipeId", "dbo.Recipes");
            DropForeignKey("dbo.Comments", "ReceipeId", "dbo.Recipes");
            DropForeignKey("dbo.UserProfiles", "Id", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Recipes", "UserId", "dbo.Users");
            DropForeignKey("dbo.Comments", "UserId", "dbo.Users");
            DropForeignKey("dbo.Steps", "ReceipeId", "dbo.Recipes");
            DropForeignKey("dbo.Tags", "CategoryId", "dbo.Categories");
            DropIndex("dbo.TagRecipes", new[] { "TagId" });
            DropIndex("dbo.TagRecipes", new[] { "RecipeId" });
            DropIndex("dbo.RecipeRatings", new[] { "ReceipeId" });
            DropIndex("dbo.RecipeRatings", new[] { "UserId" });
            DropIndex("dbo.RecipeIngredients", new[] { "MeasureId" });
            DropIndex("dbo.RecipeIngredients", new[] { "ReceipeId" });
            DropIndex("dbo.RecipeIngredients", new[] { "IngredientId" });
            DropIndex("dbo.FavouriteRecipes", new[] { "ReceipeId" });
            DropIndex("dbo.FavouriteRecipes", new[] { "UserId" });
            DropIndex("dbo.UserProfiles", new[] { "Id" });
            DropIndex("dbo.Users", new[] { "RoleId" });
            DropIndex("dbo.Steps", new[] { "ReceipeId" });
            DropIndex("dbo.Recipes", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "UserId" });
            DropIndex("dbo.Comments", new[] { "ReceipeId" });
            DropIndex("dbo.Tags", new[] { "CategoryId" });
            DropTable("dbo.TagRecipes");
            DropTable("dbo.RecipeRatings");
            DropTable("dbo.RecipeIngredients");
            DropTable("dbo.Measures");
            DropTable("dbo.Ingredients");
            DropTable("dbo.FavouriteRecipes");
            DropTable("dbo.UserProfiles");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Steps");
            DropTable("dbo.Recipes");
            DropTable("dbo.Comments");
            DropTable("dbo.Tags");
            DropTable("dbo.Categories");
        }
    }
}
