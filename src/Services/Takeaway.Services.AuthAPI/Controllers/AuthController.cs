using Microsoft.AspNetCore.Mvc;
using Takeaway.Services.AuthAPI.Models.Dtos;
using Takeaway.Services.AuthAPI.Repositories;

namespace Takeaway.Services.AuthAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly ILogger<AuthController> _logger;
        private readonly ResponseDto _responseDto;

        public AuthController(IAuthRepository authRepository, ILogger<AuthController> logger)
        {
            _authRepository = authRepository;
            _responseDto = new ResponseDto();
            _logger = logger;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            try
            {
                var result = await _authRepository.RegisterAsync(registerRequestDto);
                if (string.IsNullOrWhiteSpace(result))
                {
                    return Ok(_responseDto);
                }
                _responseDto.IsSuccess = false;
                _responseDto.Message = result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "注册失败;{registerRequestDto}", registerRequestDto);
                _responseDto.IsSuccess = false;
                _responseDto.Message = "注册失败";
            }
            return BadRequest(_responseDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginRequestDto loginRequestDto)
        {
            var result = await _authRepository.LoginAsync(loginRequestDto);
            if (result.UserDto == null)
            {
                _responseDto.IsSuccess = false;
                _responseDto.Message = "用户名或密码错误";
                return BadRequest(_responseDto);
            }
            else
            {
                _responseDto.Result = result;
                return Ok(_responseDto);
            }
        }
    }
}
