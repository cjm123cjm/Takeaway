namespace Takeaway.Web.Models.Auth
{
    public class LoginResponseDto
    {
        public UserDto? UserDto { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
