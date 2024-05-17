using System.ComponentModel.DataAnnotations;

namespace Takeaway.Web.Models.Auth
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "用户名不能为空")]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; } = null!;
    }
}
