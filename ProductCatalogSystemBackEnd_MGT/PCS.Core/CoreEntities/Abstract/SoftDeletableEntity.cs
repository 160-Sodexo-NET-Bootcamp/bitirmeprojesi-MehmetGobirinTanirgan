namespace PCS.Core.CoreEntities.Abstract
{
    public abstract class SoftDeletableEntity<TPk> : HardDeletableEntity<TPk>,ISoftDeletable
    {
        public bool IsDeleted { get; set; }
    }
}
