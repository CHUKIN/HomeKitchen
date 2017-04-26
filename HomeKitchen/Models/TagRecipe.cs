using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeKitchen.Models
{
    public class TagRecipe
    {
        //[Key]
        //[ForeignKey("Recipe"), Column(Order = 0)]
        //public int RecipeId { get; set; }
        //public Recipe Recipe { get; set; }

        //[Key]
        //[ForeignKey("Tag"), Column(Order = 1)]
        //public int TagId { get; set; }
        //public  Tag Tag { get; set; }

        public int Id { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public int TagId { get; set; }
        public Tag Tag { get; set;} 

    }
}