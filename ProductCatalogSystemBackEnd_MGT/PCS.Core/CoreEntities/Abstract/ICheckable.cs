namespace PCS.Core.CoreEntities.Abstract
{
    public interface ICheckable
    {
        string CreatedBy { get; set; }
        string CreatedById { get; set; }
        string UpdatedBy { get; set; }
        string UpdatedById { get; set; }
    }
}
