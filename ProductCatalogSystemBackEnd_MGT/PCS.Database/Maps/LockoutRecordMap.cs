using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCS.Core.CoreEntityMaps;
using PCS.Entity.Models;
using System;

namespace PCS.Database.Maps
{
    class LockoutRecordMap : SoftDeletableEntityMap<LockoutRecord, Guid>
    {
        public override void Configure(EntityTypeBuilder<LockoutRecord> builder)
        {
            builder.Property(x => x.LockoutBegin).IsRequired();
            builder.Property(x => x.LockoutUntil).IsRequired();
            builder.Property(x => x.UserId).IsRequired();

            builder.HasOne(l => l.User)
                .WithMany(u => u.LockoutRecords)
                .HasForeignKey(l => l.UserId);

            base.Configure(builder);
        }
    }
}
