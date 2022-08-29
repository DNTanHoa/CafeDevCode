using CafeDevCode.Logic.Commands.Request;
using System.ComponentModel.DataAnnotations;

namespace CafeDevCode.Website.Models
{
    public class ChangePasswordViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
        public string? UserName { get; set; }
        [Required(ErrorMessage = "Tên đăng cũ không được để trống")]
        public string? OldPassword { get; set; }
        [Required(ErrorMessage = "Mật khẩu mới không được để trống")]
        public string? NewPassword { get; set; }
        [Required(ErrorMessage = "Xác nhận mật khẩu không được để trống")]
        [Compare("NewPassword", ErrorMessage = "Xác nhận mật khẩu không khớp")]
        public string? ConfirmPassword { get; set; }

        public string? ErrorMessage { get; set; }

        public ChangePassword ToChangePasswordCommand()
        {
            return new ChangePassword()
            {
                ChangPasswordUserName = this.UserName,
                OldPassowrd = this.OldPassword,
                NewPassowrd = this.NewPassword,
            };
        }

    }
}
