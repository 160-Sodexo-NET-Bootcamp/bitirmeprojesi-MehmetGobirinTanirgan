using Microsoft.AspNetCore.Http;
using PCS.Core.CoreEntities.Abstract;
using PCS.Core.Utils.Abstract;
using System;

namespace PCS.Core.Utils.Concrete
{
    public class CheckableEntityHelper : ICheckableEntityHelper
    {
        public Guid MainUserId { get; }
        public string MainUserFullname { get; }

        public CheckableEntityHelper(IHttpContextAccessor httpContextAccessor)
        {
            MainUserId = httpContextAccessor.HttpContext.User.GetMainUserId();
            MainUserFullname = httpContextAccessor.HttpContext.User.GetMainUserFullname();
        }

        public void AddMainUserAsCreator(ICheckable entity)
        {
            entity.CreatedById = MainUserId.ToString();
            entity.CreatedBy = MainUserFullname;
        }

        public void AddMainUserAsUpdater(ICheckable entity)
        {
            entity.UpdatedById = MainUserId.ToString();
            entity.UpdatedBy = MainUserFullname;
        }

        public void AddCustomDataAsCreator(ICheckable entity, string createdById, string createdBy)
        {
            entity.CreatedById = createdById;
            entity.CreatedBy = createdBy;
        }

        public void AddCustomDataAsUpdater(ICheckable entity, string updatedById, string updatedBy)
        {
            entity.UpdatedById = updatedById;
            entity.UpdatedBy = updatedBy;
        }
    }
}
