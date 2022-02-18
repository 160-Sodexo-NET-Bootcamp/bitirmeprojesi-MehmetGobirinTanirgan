using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCS.Core.CoreEntityMaps;
using PCS.Entity.Models;
using System;

namespace PCS.Database.Maps
{
    class EmailMap : SoftDeletableEntityMap<Email, Guid>
    {
        public override void Configure(EntityTypeBuilder<Email> builder)
        {
            builder.Property(x => x.EmailStatus).IsRequired();
            builder.Property(x => x.EmailType).IsRequired();
            builder.Property(x => x.ReceiverId).IsRequired();
            builder.Property(x => x.AttemptCount).IsRequired();
            builder.Property(x => x.MailResponse).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.MailResponseDate).IsRequired(false);
            builder.Property(x => x.IsMoved).IsRequired();

            builder.HasOne(e => e.Receiver)
                .WithMany(u => u.Emails)
                .HasForeignKey(e => e.ReceiverId);       
            base.Configure(builder);
        }
    }
}
