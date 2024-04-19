namespace Takeaway.Services.AuthAPI.Models.Dtos
{
    public class LoginResponseDto
    {
        public UserDto? UserDto { get; set; }
        public string Token { get; set; } = string.Empty;
    }
}
