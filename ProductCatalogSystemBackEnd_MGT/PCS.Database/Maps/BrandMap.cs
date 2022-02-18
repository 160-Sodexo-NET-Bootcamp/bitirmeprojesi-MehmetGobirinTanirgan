using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCS.Core.CoreEntityMaps;
using PCS.Entity.Models;
using System;

namespace PCS.Database.Maps
{
    class BrandMap : SoftDeletableEntityMap<Brand, Guid>
    {
        public override void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.Property(x => x.BrandName).IsRequired().HasMaxLength(100);
            base.Configure(builder);
        }
    }
}
