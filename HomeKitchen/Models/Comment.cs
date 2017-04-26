using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeKitchen.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public int ReceipeId { get; set; }
        public Recipe Receipe { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public DateTime Date { get; set; }

    }
}