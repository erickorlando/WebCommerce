using System;
using System.Collections.Generic;

namespace WebCommerce.DataAccess
{
    public partial class Genre
    {
        public Genre()
        {
            Concerts = new HashSet<Concert>();
        }

        public int Id { get; set; }
        public string Description { get; set; } = null!;
        public bool? Status { get; set; }

        public virtual ICollection<Concert> Concerts { get; set; }
    }
}
