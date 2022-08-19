using System;
using System.Collections.Generic;

namespace Reselling.Models
{
    public partial class Ordere
    {
        public Ordere()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public string? Adress { get; set; }
        public string? Status { get; set; }
        public string? Phone { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public string? UserId { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
