using Microsoft.EntityFrameworkCore;
using WebCommerce.Entities;

namespace WebCommerce.DataAccess
{
    public class WebCommerceDbContext : DbContext
    {
        public WebCommerceDbContext(DbContextOptions<WebCommerceDbContext> options)
            :base(options)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Fluent API

            modelBuilder.Entity<Category>()
                .Property(p => p.Name)
                .HasMaxLength(50);
            
            modelBuilder.Entity<Category>()
                .Property(p => p.Description)
                .HasMaxLength(150);
        }
    }
}