namespace PCS.Entity.Dtos.UserDtos.Request
{
    public class UserCreateDto
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
