using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeKitchen.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Preview { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime CookingTime { get; set; }
        public DateTime DateOfCreation { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }

        public virtual ICollection<Step> Steps { get; set; }
        public virtual ICollection<TagRecipe> Tags { get; set; }
        public virtual ICollection<RecipeIngredient> RecipeIngredient { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<FavouriteRecipe> FavouriteRecipe { get; set; }
        public virtual ICollection<RecipeRating> RecipeRating { get; set; }

        public Recipe()
        {
            Steps = new List<Step>();
            Tags = new List<TagRecipe>();
            RecipeIngredient = new List<RecipeIngredient>();
            Comments =new List<Comment>();
            FavouriteRecipe = new List<FavouriteRecipe>();
            RecipeRating = new List<RecipeRating>();
        }
    }
}