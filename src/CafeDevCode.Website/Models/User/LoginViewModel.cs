using CafeDevCode.Logic.Commands.Request;
using System.ComponentModel.DataAnnotations;

namespace CafeDevCode.Website.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không thể bỏ trống")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Mật khẩu không thể bỏ trống")]
        public string? PassWord { get; set; }
        public bool RememberPassWord { get; set; }
        public string? ReturnUrl { get; set; }

        public Login ToCommand()
        {
            return new Login()
            {
                UserName = UserName,
                Password = PassWord,
                RememberPassword = RememberPassWord
            };
        }

        public string? ErrorMessage { get; set; }
    }
}
