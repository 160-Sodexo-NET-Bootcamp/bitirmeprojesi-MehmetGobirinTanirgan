using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCS.Core.CoreEntityMaps;
using PCS.Entity.Models;
using System;

namespace PCS.Database.Maps
{
    class FailedEmailMap : SoftDeletableEntityMap<FailedEmail, Guid>
    {
        public override void Configure(EntityTypeBuilder<FailedEmail> builder)
        {
            builder.Property(x => x.EmailId).IsRequired();
            builder.Property(x => x.FinalStatus).IsRequired(false);

            builder.HasOne(f => f.Email)
                .WithOne(e => e.FailedEmail)
                .HasForeignKey<FailedEmail>(f => f.EmailId);
            base.Configure(builder);
        }
    }
}
