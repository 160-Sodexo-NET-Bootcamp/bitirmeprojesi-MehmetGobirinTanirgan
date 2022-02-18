using System;

namespace PCS.Core.CoreEntities.Abstract
{
    // Core entity
    public abstract class HardDeletableEntity<TPk> : ICoreEntity<TPk>, IDateSign, ICheckable
    {
        public TPk Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string CreatedBy { get; set; } = "Initial Create";
        public string CreatedById { get; set; } = "Initial Create";
        public string UpdatedBy { get; set; }
        public string UpdatedById { get; set; }
    }
}
