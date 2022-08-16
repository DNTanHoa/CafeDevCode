using CafeDevCode.Logic.Commands.Request;
using System.ComponentModel.DataAnnotations;

namespace CafeDevCode.Website.Models
{
    public class UserDetailViewModel : BaseViewModel
    {
        [Required(ErrorMessage = "Tài khoản không được để trống")]
        public string? DetailUserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? AuthorId { get; set; }
        public string? Password { get; set; }

        public CreateUser ToCreateCommand()
        {
            return new CreateUser()
            {
                CreateUserName = this.DetailUserName ?? string.Empty,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                AuthorId = this.AuthorId,
                Password = this.Password,
            };
        }

        public UpdateUser ToUpdateCommand()
        {
            return new UpdateUser()
            {
                UpdateUserName = DetailUserName ?? string.Empty,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                AuthorId = AuthorId,
            };
        }
    }
}
