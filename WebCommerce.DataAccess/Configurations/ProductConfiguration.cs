﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebCommerce.Entities;

namespace WebCommerce.DataAccess.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.Property(p => p.Name)
            .HasMaxLength(100);

        builder.Property(p => p.UnitPrice)
            .HasPrecision(11, 2);

        builder.Property(p => p.Description)
            .HasMaxLength(200);

        builder.Property(p => p.ImageUrl)
            .HasMaxLength(1000)
            .IsUnicode(false);
    }
}