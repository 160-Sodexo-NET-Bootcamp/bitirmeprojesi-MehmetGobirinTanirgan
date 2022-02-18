using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCS.Core.CoreEntityMaps;
using PCS.Entity.Models;
using System;

namespace PCS.Database.Maps
{
    class CategoryMap : SoftDeletableEntityMap<Category, Guid>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.CategoryName).IsRequired().HasMaxLength(50);
            builder.Property(x => x.CategoryLevel).IsRequired();
            builder.Property(x => x.LeftBorder).IsRequired();
            builder.Property(x => x.RightBorder).IsRequired();
            base.Configure(builder);
        }
    }
}
