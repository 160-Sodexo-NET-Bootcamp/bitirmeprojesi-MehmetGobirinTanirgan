using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCS.Core.CoreEntityMaps;
using PCS.Entity.Models;
using System;

namespace PCS.Database.Maps
{
    class UserMap : SoftDeletableEntityMap<User, Guid>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Firstname).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Lastname).IsRequired().HasMaxLength(50);
            builder.Property(x => x.PhoneNumber).IsRequired().HasMaxLength(20);
            builder.Property(x => x.EmailAddress).IsRequired().HasMaxLength(100);
            builder.Property(x => x.RefreshToken).IsRequired(false).HasMaxLength(100);
            builder.Property(x => x.RefreshTokenExpireDate).IsRequired(false);
            builder.Property(x => x.LastLoginIP).IsRequired(false).HasMaxLength(50);
            builder.Property(x => x.HashedPassword).IsRequired();
            builder.Property(x => x.PasswordSalt).IsRequired();
            builder.Property(x => x.EmailConfirmed).IsRequired();
            builder.Property(x => x.Role).IsRequired().HasMaxLength(30);
            builder.Property(x => x.FailedLoginAttemptCount).IsRequired();
            builder.Property(x => x.LockoutEnabled).IsRequired();
            builder.Property(x => x.DailyLockoutCount).IsRequired();
            builder.Property(x => x.PermaBlockEnabled).IsRequired();
            base.Configure(builder);
        }
    }
}
