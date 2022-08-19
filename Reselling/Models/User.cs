using System;
using System.Collections.Generic;

namespace Reselling.Models
{
    public partial class User
    {
        public User()
        {
            Books = new HashSet<Book>();
            Orderes = new HashSet<Ordere>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Adress { get; set; }

        public virtual AspNetUser IdNavigation { get; set; } = null!;
        public virtual ICollection<Book> Books { get; set; }
        public virtual ICollection<Ordere> Orderes { get; set; }
    }
}
