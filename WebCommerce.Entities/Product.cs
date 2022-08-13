namespace WebCommerce.Entities;

public class Product : BaseEntity
{
    public Category Category { get; set; } = null!;

    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public decimal UnitPrice { get; set; }

    public bool Discontinued { get; set; }
}