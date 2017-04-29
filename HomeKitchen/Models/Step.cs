using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HomeKitchen.Models
{
    public class Step
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public string PhotoUrl { get; set; }

        public int ReceipeId { get; set; }
        public virtual Recipe Receipe { get; set; }
    }
}