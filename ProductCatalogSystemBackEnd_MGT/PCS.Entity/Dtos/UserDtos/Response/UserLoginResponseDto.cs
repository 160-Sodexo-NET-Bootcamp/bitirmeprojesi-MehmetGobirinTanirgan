using System;

namespace PCS.Entity.Dtos.UserDtos.Response
{
    public class UserLoginResponseDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string AuthenticationToken { get; set; }
        public DateTime AuthenticationTokenExpireDate { get; set; }
        public string RefreshToken { get; set; }
    }
}
