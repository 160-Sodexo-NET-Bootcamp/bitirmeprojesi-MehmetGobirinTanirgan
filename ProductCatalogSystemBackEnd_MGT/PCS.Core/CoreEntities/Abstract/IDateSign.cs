using System;

namespace PCS.Core.CoreEntities.Abstract
{
    public interface IDateSign
    {
        DateTime CreatedDate { get; set; }
        DateTime? UpdatedDate { get; set; }
    }
}
