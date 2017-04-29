using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeKitchen.Models
{
    public class RecipeIngredient
    {
        


        [Key]
        [ForeignKey("Ingredient"), Column(Order = 0)]
        public int IngredientId { get; set; }
        public virtual Ingredient Ingredient { get; set; }

        [Key]
        [ForeignKey("Recipe"), Column(Order = 1)]
        public int ReceipeId { get; set; }
        public virtual Recipe Recipe { get; set; }

        public int MeasureId { get; set; }
        public virtual Measure Measure { get; set; }

        public double Amount { get; set; }
    }
}