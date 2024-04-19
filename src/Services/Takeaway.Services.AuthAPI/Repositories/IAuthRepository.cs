using Takeaway.Services.AuthAPI.Models.Dtos;

namespace Takeaway.Services.AuthAPI.Repositories
{
    public interface IAuthRepository
    {
        Task<string> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<LoginResponseDto> LoginAsync(LoginRequestDto loginRequestDto);
    }
}
