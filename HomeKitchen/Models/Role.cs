using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeKitchen.Models
{
    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<User> Users { get; set; }

        public Role()
        {
            Users = new List<User>();
        }
    }
}