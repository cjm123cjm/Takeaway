using Takeaway.Web.Models;
using Takeaway.Web.Models.Auth;
using Takeaway.Web.Service.IService;
using Takeaway.Web.Utility;

namespace Takeaway.Web.Service
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService;
        }
        public async Task<ResponseDto?> AssigenRoleAsync(RegisterRequestDto registerRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthAPIBase + "/api/auth/assginrole",
                Data = registerRequestDto
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequest)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthAPIBase + "/api/auth/login",
                Data = loginRequest
            });
        }

        public async Task<ResponseDto?> RegisterAsync(RegisterRequestDto registerRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = SD.ApiType.POST,
                Url = SD.AuthAPIBase + "/api/auth/register",
                Data = registerRequestDto
            });
        }
    }
}
