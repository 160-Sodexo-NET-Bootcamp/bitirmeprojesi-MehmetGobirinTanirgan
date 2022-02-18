using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCS.Core.CoreEntityMaps;
using PCS.Entity.Models;

namespace PCS.Database.Maps
{
    class PaymentTypeMap : HardDeletableEntityMap<PaymentType, int>
    {
        public override void Configure(EntityTypeBuilder<PaymentType> builder)
        {
            builder.Property(x => x.PaymentMethod).IsRequired().HasMaxLength(50);
            base.Configure(builder);
        }
    }
}
