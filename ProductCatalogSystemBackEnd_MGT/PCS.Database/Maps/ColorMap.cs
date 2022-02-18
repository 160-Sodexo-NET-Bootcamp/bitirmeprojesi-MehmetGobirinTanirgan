using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCS.Core.CoreEntityMaps;
using PCS.Entity.Models;

namespace PCS.Database.Maps
{
    class ColorMap : HardDeletableEntityMap<Color, int>
    {
        public override void Configure(EntityTypeBuilder<Color> builder)
        {
            builder.Property(x => x.ColorName).IsRequired().HasMaxLength(50);
            base.Configure(builder);
        }
    }
}
