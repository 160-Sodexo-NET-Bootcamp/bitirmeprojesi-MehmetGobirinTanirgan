using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PCS.Core.CoreEntities.Abstract;

namespace PCS.Core.CoreEntityMaps
{
    public class SoftDeletableEntityMap<TEntity, TPk> : HardDeletableEntityMap<TEntity, TPk> where TEntity : SoftDeletableEntity<TPk>
    {
        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.Property(x => x.IsDeleted).IsRequired();
        }
    }
}
