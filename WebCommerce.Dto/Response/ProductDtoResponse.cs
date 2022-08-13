namespace WebCommerce.Dto.Response;

/* COMO CLASE */
//public class ProductDtoResponse
//{
//    public int Id { get; set; }
//    public string Name { get; set; }
//    public decimal UnitPrice { get; set; }
//    public string CategoryName { get; set; }
//}

/* COMO RECORD */

public record ProductDtoResponse(int Id, string Name, decimal UnitPrice, string CategoryName);