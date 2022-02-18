using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCS.Core.CoreEntityMaps;
using PCS.Entity.Models;
using System;

namespace PCS.Database.Maps
{
    class OrderMap : SoftDeletableEntityMap<Order, Guid>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.Property(x => x.OrderPrice).IsRequired().HasColumnType("decimal(8,2)");
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.PaymentTypeId).IsRequired();

            builder.HasOne(o => o.Product)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.ProductId);

            builder.HasOne(o => o.PaymentType)
                .WithMany(p => p.Orders)
                .HasForeignKey(o => o.PaymentTypeId);
            base.Configure(builder);
        }
    }
}
