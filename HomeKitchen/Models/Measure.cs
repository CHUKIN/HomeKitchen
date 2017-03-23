using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeKitchen.Models
{
    public class Measure
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<RecipeIngredient> RecipeIngredients { get; set; }

        public Measure()
        {
            RecipeIngredients = new List<RecipeIngredient>();
        }
    }
}   