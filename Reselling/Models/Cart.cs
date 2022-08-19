using System;
using System.Collections.Generic;

namespace Reselling.Models
{
    public partial class Cart
    {
        public int Id { get; set; }
        public int? BookId { get; set; }
    }
}
