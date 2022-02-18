namespace PCS.Core.CoreEntities.Abstract
{
    public interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
