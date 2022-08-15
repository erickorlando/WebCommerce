using AutoMapper;
using WebCommerce.Dto.Request;
using WebCommerce.Dto.Response;
using WebCommerce.Entities;
using WebCommerce.Entities.Infos;

namespace WebCommerce.Services.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductSingleDto>();
        
        CreateMap<ProductDtoRequest, Product>();

        CreateMap<ProductInfo, ProductDtoResponse>();
    }

}