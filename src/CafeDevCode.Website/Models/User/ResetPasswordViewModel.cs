using CafeDevCode.Logic.Commands.Request;
using System.ComponentModel.DataAnnotations;

namespace CafeDevCode.Website.Models
{
    public class ResetPasswordViewModel : BaseViewModel
    {
        public string ResetUserName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string NewPassword { get; set; } = string.Empty;

        public ResetPassword ToResetPasswordCommand()
        {
            return new ResetPassword()
            {
                ResetUserName = ResetUserName,
                NewPassword = NewPassword
            };
        }
    }
}
