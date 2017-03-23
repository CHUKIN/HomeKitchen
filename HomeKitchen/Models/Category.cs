using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeKitchen.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Tag> Tags { get; set; }

        public Category()
        {
            Tags = new List<Tag>();
        }
    }
}