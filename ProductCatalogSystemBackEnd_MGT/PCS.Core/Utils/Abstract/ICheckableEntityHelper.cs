using PCS.Core.CoreEntities.Abstract;
using System;

namespace PCS.Core.Utils.Abstract
{
    public interface ICheckableEntityHelper
    {
        Guid MainUserId { get; }
        string MainUserFullname { get; }
        void AddMainUserAsCreator(ICheckable entity);
        void AddMainUserAsUpdater(ICheckable entity);
        void AddCustomDataAsCreator(ICheckable entity, string createdById, string createdBy);
        void AddCustomDataAsUpdater(ICheckable entity, string updatedById, string updatedBy);
    }
}
