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
        public Role Role { get; set; }

        public UserProfile UserProfile { get; set; }

        public ICollection<Comment> Comments { get; set; }

        public ICollection<Recipe> Recipies { get; set; }

        public User()
        {
            Comments = new List<Comment>();
            Recipies = new List<Recipe>();
        }
    }
}