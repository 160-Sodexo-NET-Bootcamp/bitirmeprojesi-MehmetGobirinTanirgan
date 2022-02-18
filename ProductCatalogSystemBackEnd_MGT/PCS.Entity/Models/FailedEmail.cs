using PCS.Core.CoreEntities.Abstract;
using PCS.Entity.Enums;
using System;

namespace PCS.Entity.Models
{
    public class FailedEmail : SoftDeletableEntity<Guid>
    {
        public Guid EmailId { get; set; }
        public Email Email { get; set; }
        public EmailStatus? FinalStatus { get; set; }
    }
}
