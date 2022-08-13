using System.ComponentModel.DataAnnotations;

namespace WebCommerce.Dto.Request;

public class ProductDtoRequest
{
    public int CategoryId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }
    
    public decimal UnitPrice { get; set; }
}