using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HomeKitchen.Models
{
    public class FavouriteRecipe
    {
        [Key]
        [ForeignKey("User"), Column(Order = 0)]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Key]
        [ForeignKey("Recipe"), Column(Order = 1)]
        public int ReceipeId { get; set; }
        public virtual Recipe Recipe { get; set; }
    }
}