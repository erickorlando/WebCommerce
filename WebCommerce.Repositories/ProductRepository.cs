using Microsoft.EntityFrameworkCore;
using WebCommerce.DataAccess;
using WebCommerce.Entities;
using WebCommerce.Entities.Infos;

namespace WebCommerce.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebCommerceDbContext _context;

        public ProductRepository(WebCommerceDbContext context)
        {
            _context = context;
        }

        
        public async Task<ICollection<ProductInfo>> ListAsync()
        {
            return await _context.Set<Product>()
                .Where(p => p.Status)
                .Select(x =>
                    new ProductInfo(x.Id,
                        x.Name,
                        x.UnitPrice,
                        x.Category.Name))
                .AsNoTracking() // Permite traer los datos sin seguimiento.
                .ToListAsync();
        }
    }
}