using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace HomeKitchen.Models
{
    public class KitchenContext : DbContext
    {
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<FavouriteRecipe> FavouriteRecipies { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<Measure> Measuries { get; set; }
        public DbSet<Recipe> Recipies { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<RecipeRating> RecipeRatings { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagRecipe> TagRecipies { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserProfile> UserProfilies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}