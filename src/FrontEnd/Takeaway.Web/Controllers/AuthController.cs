using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Takeaway.Web.Models;
using Takeaway.Web.Models.Auth;
using Takeaway.Web.Service.IService;
using Takeaway.Web.Utility;

namespace Takeaway.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;

        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new LoginRequestDto();
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            if (ModelState.IsValid)
            {
                var responseDto = await _authService.LoginAsync(loginRequestDto);
                if (responseDto != null && responseDto.IsSuccess)
                {
                    LoginResponseDto loginResponseDto = JsonConvert.DeserializeObject<LoginResponseDto>(Convert.ToString(responseDto.Result));

                    _tokenProvider.SetToken(loginResponseDto.Token);
                    await SingInUser(loginResponseDto);

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("Login", "登陆失败");
                    return View(loginRequestDto);
                }
            }

            return View(loginRequestDto);
        }

        [HttpGet]
        public IActionResult Register()
        {
            RegisterRequestDto registerRequestDto = new RegisterRequestDto();

            ViewBag.RoleList = GetRoleList();

            return View(registerRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            if (ModelState.IsValid)
            {
                ResponseDto? responseDto = await _authService.RegisterAsync(registerRequestDto);
                ResponseDto roleDto = null;
                if (responseDto != null && responseDto.IsSuccess)
                {
                    if (string.IsNullOrWhiteSpace(registerRequestDto.Role))
                    {
                        registerRequestDto.Role = SD.RoleCustomer;
                    }
                    roleDto = await _authService.AssigenRoleAsync(registerRequestDto);
                    if (roleDto != null && roleDto.IsSuccess)
                    {
                        TempData["success"] = "注册成功";
                        //跳转到主页
                        return RedirectToAction("Index", "Home");
                    }

                }
                ViewBag.RoleList = GetRoleList();
                ModelState.AddModelError("Register", responseDto.Message);
                return View(registerRequestDto);
            }
            return View(registerRequestDto);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }
        private List<SelectListItem> GetRoleList()
        {
            var selectList = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Text=SD.RoleCustomer,
                    Value=SD.RoleName
                },
                new SelectListItem
                {
                    Text=SD.RoleCustomer,
                    Value=SD.RoleCustomer
                },
            };
            return selectList;
        }

        private async Task SingInUser(LoginResponseDto loginResponseDto)
        {
            var handler = new JwtSecurityTokenHandler();

            var jwt = handler.ReadJwtToken(loginResponseDto.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(
                JwtRegisteredClaimNames.Email,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)!.Value));
            identity.AddClaim(new Claim(
                JwtRegisteredClaimNames.Sub,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)!.Value));
            identity.AddClaim(new Claim(
                JwtRegisteredClaimNames.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name)!.Value));
            identity.AddClaims(jwt.Claims.Where(u => u.Type == ClaimTypes.Role).ToList());

            identity.AddClaim(new Claim(
                ClaimTypes.Name,
                jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)!.Value));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
