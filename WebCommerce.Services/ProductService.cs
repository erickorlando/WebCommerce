using Microsoft.Extensions.Logging;
using WebCommerce.Dto.Response;
using WebCommerce.Repositories;

namespace WebCommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository repository, ILogger<ProductService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<BaseResponseGeneric<List<ProductDtoResponse>>> ListAsync()
        {
            var response = new BaseResponseGeneric<List<ProductDtoResponse>>();

            try
            {
                var query = await _repository.ListAsync();
                response.Data = query
                    .Select(x => new ProductDtoResponse(x.Id, x.Name, x.UnitPrice, x.CategoryName))
                    .ToList();
                
                response.Success = true;

                _logger.LogInformation("Se hizo la consulta a la BD");
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
}
}