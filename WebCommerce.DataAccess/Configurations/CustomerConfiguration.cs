using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCommerce.Entities;

namespace WebCommerce.DataAccess.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>

{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        throw new NotImplementedException();
    }
}