using System;
using System.Collections.Generic;

namespace WebCommerce.DataAccess
{
    public partial class Concert
    {
        public Concert()
        {
            Sales = new HashSet<Sale>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateEvent { get; set; }
        public int TicketsQuantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string? ImageUrl { get; set; }
        public string? Place { get; set; }
        public int GenreId { get; set; }
        public bool Finalized { get; set; }
        public bool? Status { get; set; }
        public string? Summary { get; set; }
        
        public virtual Genre Genre { get; set; } = null!;
        public virtual ICollection<Sale> Sales { get; set; }
    }
}
