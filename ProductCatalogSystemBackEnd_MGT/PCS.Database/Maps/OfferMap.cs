using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCS.Core.CoreEntityMaps;
using PCS.Entity.Models;
using System;

namespace PCS.Database.Maps
{
    class OfferMap : SoftDeletableEntityMap<Offer, Guid>
    {
        public override void Configure(EntityTypeBuilder<Offer> builder)
        {
            builder.Property(x => x.OfferStatus).IsRequired();
            builder.Property(x => x.OfferPercentage).IsRequired();
            builder.Property(x => x.ProductId).IsRequired();
            builder.Property(x => x.ProductOwnerId).IsRequired();
            builder.Property(x => x.OffererId).IsRequired();

            builder.HasOne(o => o.Offerer)
                .WithMany(u => u.GivenOffers)
                .HasForeignKey(o => o.OffererId);

            builder.HasOne(o => o.ProductOwner)
                .WithMany(u => u.TakenOffers)
                .HasForeignKey(o => o.ProductOwnerId);

            builder.HasOne(o => o.Product)
                .WithMany(p => p.ProductOffers)
                .HasForeignKey(o => o.ProductId);

            base.Configure(builder);
        }
    }
}
