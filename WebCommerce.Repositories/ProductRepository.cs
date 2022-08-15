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
                        x.Category.Name,
                        x.ImageUrl))
                .AsNoTracking() // Permite traer los datos sin seguimiento.
                .ToListAsync();
        }

        public async Task<ICollection<ProductInfo>> ListAsync(string filter, int page, int rows)
        {
            return await _context.Set<Product>()
                .Where(p => p.Status && p.Name.Contains(filter))
                .Skip((page - 1) * rows)
                .Take(rows)
                .OrderBy(p => p.Id)
                .Select(x =>
                    new ProductInfo(x.Id,
                        x.Name,
                        x.UnitPrice,
                        x.Category.Name,
                        x.ImageUrl))
                .AsNoTracking() // Permite traer los datos sin seguimiento.
                .ToListAsync();
        }

        public async Task<Product?> GetAsync(int id)
        {
            return await _context.Set<Product>()
                .FindAsync(id);
        }

        public async Task<int> AddAsync(Product product)
        {
            await _context.Set<Product>().AddAsync(product);
            await _context.SaveChangesAsync();
            return product.Id;
        }

        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);

            if (entity != null)
            {
                entity.Status = false;
                await _context.SaveChangesAsync();
            }
        }
    }
}