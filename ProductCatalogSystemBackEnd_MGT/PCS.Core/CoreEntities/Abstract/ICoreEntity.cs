using System;

namespace PCS.Core.CoreEntities.Abstract
{
    public interface ICoreEntity<TPk>
    {
        TPk Id { get; set; }
    }
}
