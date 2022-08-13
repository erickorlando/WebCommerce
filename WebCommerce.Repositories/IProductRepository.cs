using WebCommerce.Entities.Infos;

namespace WebCommerce.Repositories;

public interface IProductRepository
{
    Task<ICollection<ProductInfo>> ListAsync();
}