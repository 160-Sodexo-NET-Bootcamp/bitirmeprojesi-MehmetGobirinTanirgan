using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCS.Core.CoreEntities.Abstract;

namespace PCS.Core.CoreEntityMaps
{
    //Core entity map
    public class HardDeletableEntityMap<TEntity, TPk> : IEntityTypeConfiguration<TEntity> where TEntity : HardDeletableEntity<TPk>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.CreatedDate).IsRequired();
            builder.Property(x => x.UpdatedDate).IsRequired(false);
            builder.Property(x => x.CreatedBy).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.CreatedById).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.UpdatedBy).IsRequired(false).HasMaxLength(200);
            builder.Property(x => x.UpdatedById).IsRequired(false).HasMaxLength(200);
        }
    }
}
