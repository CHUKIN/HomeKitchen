using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeKitchen.Models
{
    public class RecipeAjax
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Preview { get; set; }
        public string PhotoUrl { get; set; }

        public bool Favourite { get; set; }
        public int Likes { get; set; } 

     
    }
}