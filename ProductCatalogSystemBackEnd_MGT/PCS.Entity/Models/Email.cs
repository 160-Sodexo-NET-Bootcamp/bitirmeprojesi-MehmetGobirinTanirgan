using PCS.Core.CoreEntities.Abstract;
using PCS.Entity.Enums;
using System;

namespace PCS.Entity.Models
{
    public class Email : SoftDeletableEntity<Guid>
    {
        public EmailType EmailType { get; set; }
        public EmailStatus EmailStatus { get; set; } = EmailStatus.InProcess;
        public Guid ReceiverId { get; set; }
        public User Receiver { get; set; }
        public byte AttemptCount { get; set; }
        public string MailResponse { get; set; }
        public DateTime? MailResponseDate { get; set; }
        public bool IsMoved { get; set; }
        public FailedEmail FailedEmail { get; set; }
    }
}
