using CafeDevCode.Database.Entities;
using CafeDevCode.Logic.Commands.Request;

namespace CafeDevCode.Website.Models
{
    public class RegisterViewModel : BaseViewModel
    {
        public string? RegisterUserName { get; set; } = string.Empty;
        public string? Password { get; set; } = string.Empty;
        public string? ConfirmPassword { get; set; } = string.Empty;
        public string? Email { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; } = string.Empty;
        public string? ErrorMessage { get; set; } = string.Empty;

        public CreateUser ToCreateCommand()
        {
            return new CreateUser()
            {
                CreateUserName = this.RegisterUserName ?? string.Empty,
                Email = this.Email,
                PhoneNumber = this.PhoneNumber,
                Password = this.Password,
            };
        }
    }
}
