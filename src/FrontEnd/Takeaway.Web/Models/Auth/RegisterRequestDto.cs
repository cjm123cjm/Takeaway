using System.ComponentModel.DataAnnotations;

namespace Takeaway.Web.Models.Auth
{
    public class RegisterRequestDto
    {
        [Required(ErrorMessage = "姓名不能为空")]
        [MaxLength(20, ErrorMessage = "姓名不能超过20个字符")]
        [MinLength(2, ErrorMessage = "姓名不能小于2个字符")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "邮箱不能为空")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "电话号码不能为空")]
        [Phone]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "密码不能为空")]
        public string Password { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
