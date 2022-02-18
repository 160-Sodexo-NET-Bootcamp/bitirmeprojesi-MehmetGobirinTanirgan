using PCS.Core.CoreEntities.Abstract;
using System;

namespace PCS.Entity.Models
{
    public class LockoutRecord : SoftDeletableEntity<Guid>
    {
        public DateTime LockoutBegin { get; set; }
        public DateTime LockoutUntil { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
    }
}
