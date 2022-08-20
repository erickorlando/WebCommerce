using System;
using System.Collections.Generic;

namespace WebCommerce.DataAccess
{
    public partial class Sale
    {
        public int Id { get; set; }
        public DateTime SaleDate { get; set; }
        public int ConcertId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalSale { get; set; }
        public string UserId { get; set; } = null!;
        public string OperationNumber { get; set; } = null!;
        public bool? Status { get; set; }

        public virtual Concert Concert { get; set; } = null!;
    }
}
