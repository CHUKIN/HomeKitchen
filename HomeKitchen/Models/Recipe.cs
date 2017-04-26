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
        public User User { get; set; }

        public ICollection<Step> Steps { get; set; }
        public  ICollection<TagRecipe> Tags { get; set; }
        public  ICollection<RecipeIngredient> RecipeIngredient { get; set; }
        public ICollection<Comment> Comments { get; set; }

        public Recipe()
        {
            Steps = new List<Step>();
            Tags = new List<TagRecipe>();
            RecipeIngredient = new List<RecipeIngredient>();
            Comments =new List<Comment>();
        }
    }
}