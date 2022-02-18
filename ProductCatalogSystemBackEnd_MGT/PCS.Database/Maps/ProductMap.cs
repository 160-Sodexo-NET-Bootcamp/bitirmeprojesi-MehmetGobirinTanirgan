using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCS.Core.CoreEntityMaps;
using PCS.Entity.Models;
using System;

namespace PCS.Database.Maps
{
    class ProductMap : SoftDeletableEntityMap<Product, Guid>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(x => x.ProductName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Price).IsRequired().HasColumnType("decimal(8,2)");
            builder.Property(x => x.ProductOwnerId).IsRequired();
            builder.Property(x => x.IsOfferable).IsRequired();
            builder.Property(x => x.IsSold).IsRequired();
            builder.Property(x => x.UsageCondition).IsRequired();
            builder.Property(x => x.CategoryId).IsRequired();
            builder.Property(x => x.BrandId).IsRequired(false);
            builder.Property(x => x.ColorId).IsRequired(false);

            builder.HasOne(p => p.ProductOwner)
                .WithMany(u => u.Products)
                .HasForeignKey(p => p.ProductOwnerId);

            builder.HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId);

            builder.HasOne(p => p.Brand)
                .WithMany(b => b.Products)
                .HasForeignKey(p => p.BrandId);

            builder.HasOne(p => p.Color)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.ColorId);

            base.Configure(builder);
        }
    }
}
