using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace WebCommerce.DataAccess
{
    public class WebCommerceDbContext : IdentityDbContext<WebCommerceUserIdentity>
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

            modelBuilder.Ignore("AspNetUserClaims");
            modelBuilder.Ignore("AspNetUserLogins");
        }
    }
}