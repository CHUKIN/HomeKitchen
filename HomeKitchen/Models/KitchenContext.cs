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
        public virtual DbSet<Category> Categorys { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<FavouriteRecipe> FavouriteRecipies { get; set; }
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Measure> Measuries { get; set; }
        public virtual DbSet<Recipe> Recipies { get; set; }
        public virtual DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public virtual DbSet<RecipeRating> RecipeRatings { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Step> Steps { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<TagRecipe> TagRecipies { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserProfile> UserProfilies { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}