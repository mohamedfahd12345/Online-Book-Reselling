using System;
using System.Collections.Generic;

namespace Reselling.Models
{
    public partial class Book
    {
        public Book()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public string? Category { get; set; }
        public string? Year { get; set; }
        public string? Language { get; set; }
        public string? Pages { get; set; }
        public int? Price { get; set; }
        public string? Descripition { get; set; }
        public string? Title { get; set; }
        public string? Photo { get; set; }
        public string? Author { get; set; }
        public string? UserId { get; set; }
        public string? Status { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
