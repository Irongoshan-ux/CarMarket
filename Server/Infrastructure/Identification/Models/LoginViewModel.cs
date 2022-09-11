using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace CarMarket.Server.Infrastructure.Identification.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string ReturnUrl { get; set; }
    }

    public class LoginViewModelValidator : AbstractValidator<LoginViewModel>
    {
        public LoginViewModelValidator()
        {
            RuleFor(x => x.Email).EmailAddress();
            RuleFor(x=> x.Password).NotEmpty();
        }
    }
}