using Microsoft.EntityFrameworkCore;
using System.Reflection;

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

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}