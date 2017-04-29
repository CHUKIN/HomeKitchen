using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeKitchen.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public bool Locked { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public virtual UserProfile UserProfile { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Recipe> Recipies { get; set; }

        public virtual ICollection<FavouriteRecipe> FavouriteRecipe { get; set; }

        public User()
        {
            Comments = new List<Comment>();
            Recipies = new List<Recipe>();
            FavouriteRecipe = new List<FavouriteRecipe>();
        }
    }
}