namespace PCS.Entity.Dtos.UserDtos.Request
{
    public class UserResetPasswordDto
    {
        public string EmailAddress { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
