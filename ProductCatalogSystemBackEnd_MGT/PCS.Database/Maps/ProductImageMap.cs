using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCS.Core.CoreEntityMaps;
using PCS.Entity.Models;
using System;

namespace PCS.Database.Maps
{
    class ProductImageMap : SoftDeletableEntityMap<ProductImage, Guid>
    {
        public override void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.ProductId).IsRequired();

            builder.HasOne(pi => pi.Product)
                .WithMany(p => p.ProductImages)
                .HasForeignKey(pi => pi.ProductId);

            base.Configure(builder);
        }
    }
}
