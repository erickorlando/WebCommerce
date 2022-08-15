using WebCommerce.Dto.Request;
using WebCommerce.Dto.Response;

namespace WebCommerce.Services;

public interface IProductService
{
    Task<BaseResponseGeneric<List<ProductDtoResponse>>> ListAsync();
    Task<BaseResponseGeneric<List<ProductDtoResponse>>> ListAsync(string filter, int page, int rows);

    Task<BaseResponseGeneric<ProductSingleDto>> GetByIdAsync(int id);

    Task<BaseResponseGeneric<int>> CreateAsync(ProductDtoRequest dto);

    Task<BaseResponse> UpdateAsync(int id, ProductDtoRequest dto);

    Task<BaseResponse> DeleteAsync(int id);
}