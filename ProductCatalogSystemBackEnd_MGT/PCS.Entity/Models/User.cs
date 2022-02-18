using PCS.Core.CoreEntities.Abstract;
using System;
using System.Collections.Generic;

namespace PCS.Entity.Models
{
    public class User : SoftDeletableEntity<Guid>
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public byte[] HashedPassword { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string RefreshToken { get; set; }
        public DateTime? RefreshTokenExpireDate { get; set; }
        public string LastLoginIP { get; set; }
        public bool EmailConfirmed { get; set; } = false;
        public string Role { get; set; }
        public int FailedLoginAttemptCount { get; set; }
        public bool LockoutEnabled { get; set; } = false;
        public byte DailyLockoutCount { get; set; }
        public bool PermaBlockEnabled { get; set; } = false;
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Offer> GivenOffers { get; set; }
        public IEnumerable<Offer> TakenOffers { get; set; }
        public IEnumerable<LockoutRecord> LockoutRecords { get; set; }
        public ICollection<Email> Emails { get; set; }
    }
}
