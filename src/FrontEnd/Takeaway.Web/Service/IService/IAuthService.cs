using Takeaway.Web.Models;
using Takeaway.Web.Models.Auth;

namespace Takeaway.Web.Service.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequest);
        Task<ResponseDto?> RegisterAsync(RegisterRequestDto registerRequestDto);
        Task<ResponseDto?> AssigenRoleAsync(RegisterRequestDto registerRequestDto);
    }
}
