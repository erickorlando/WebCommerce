using WebCommerce.Entities;
using WebCommerce.Entities.Infos;

namespace WebCommerce.Repositories;

public interface IProductRepository
{
    Task<ICollection<ProductInfo>> ListAsync();

    Task<ICollection<ProductInfo>> ListAsync(string filter, int page, int rows);

    Task<Product?> GetAsync(int id);

    Task<int> AddAsync(Product product);

    Task UpdateAsync();

    Task DeleteAsync(int id);
}