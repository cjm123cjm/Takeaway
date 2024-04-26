using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Takeaway.Web.Models;
using Takeaway.Web.Models.Auth;
using Takeaway.Web.Service.IService;
using Takeaway.Web.Utility;

namespace Takeaway.Web.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new LoginRequestDto();
            return View(loginRequestDto);
        }

        [HttpPost]
        public IActionResult Login(LoginRequestDto loginRequestDto)
        {
            return View();
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
                if (responseDto != null && responseDto.IsSuccess)
                {
                    //跳转到主页
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.RoleList = GetRoleList();
                    ModelState.AddModelError("Register", responseDto.Message);
                    return View(registerRequestDto);
                }
            }
            return View(registerRequestDto);
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
    }
}
