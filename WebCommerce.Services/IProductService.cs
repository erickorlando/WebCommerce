using WebCommerce.Dto.Response;

namespace WebCommerce.Services;

public interface IProductService
{
    Task<BaseResponseGeneric<List<ProductDtoResponse>>> ListAsync();
}