namespace PCS.Entity.Dtos.UserDtos.Request
{
    public class UserRefreshLoginDto
    {
        public string EmailAddress { get; set; }
        public string RefreshToken { get; set; }
    }
}
