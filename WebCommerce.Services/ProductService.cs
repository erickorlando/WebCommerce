using AutoMapper;
using Microsoft.Extensions.Logging;
using WebCommerce.Dto.Request;
using WebCommerce.Dto.Response;
using WebCommerce.Entities;
using WebCommerce.Repositories;

namespace WebCommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<ProductService> _logger;
        private readonly IMapper _mapper;
        private readonly IFileUploader _fileUploader;

        public ProductService(IProductRepository repository, ILogger<ProductService> logger, IMapper mapper, IFileUploader fileUploader)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
            _fileUploader = fileUploader;
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

        public async Task<BaseResponseGeneric<List<ProductDtoResponse>>> ListAsync(string filter, int page, int rows)
        {
            var response = new BaseResponseGeneric<List<ProductDtoResponse>>();

            try
            {
                var query = await _repository.ListAsync(filter, page, rows);
                response.Data = query
                    .Select(x => _mapper.Map<ProductDtoResponse>(x))
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

        public async Task<BaseResponseGeneric<ProductSingleDto>> GetByIdAsync(int id)
        {
            var response = new BaseResponseGeneric<ProductSingleDto>();
            try
            {
                var data = await _repository.GetAsync(id);
                if (data != null)
                    response.Data = _mapper.Map<ProductSingleDto>(data);

                response.Success = data != null;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<BaseResponseGeneric<int>> CreateAsync(ProductDtoRequest dto)
        {
            var response = new BaseResponseGeneric<int>();

            try
            {
                var product = _mapper.Map<Product>(dto);

                if (!string.IsNullOrEmpty(dto.FileName))
                {
                    product.ImageUrl = await _fileUploader.UploadFileAsync(dto.Base64Image, dto.FileName);
                }

                response.Data = await _repository.AddAsync(product);
                response.Success = true;
            }
            catch (Exception ex)
            {
                 response.Message = ex.Message;
            }
            
            return response;
        }

        public async Task<BaseResponse> UpdateAsync(int id, ProductDtoRequest dto)
        {
            var response = new BaseResponse();

            try
            {
                var entity = await _repository.GetAsync(id);

                // ID: 5
                // Name: Product 1, Description: xxxx, UnitPrice: 12.5

                if (entity != null)
                {
                    _mapper.Map(dto, entity);

                    if (!string.IsNullOrEmpty(dto.FileName))
                    {
                        entity.ImageUrl = await _fileUploader.UploadFileAsync(dto.Base64Image, dto.FileName);
                    }
                }

                await _repository.UpdateAsync();

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<BaseResponse> DeleteAsync(int id)
        {
            var response = new BaseResponse();

            try
            {
                await _repository.DeleteAsync(id);

                response.Success = true;
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }

            return response;
        }
    }
}