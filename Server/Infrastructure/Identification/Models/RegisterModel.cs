using System.ComponentModel.DataAnnotations;

namespace CarMarket.Server.Infrastructure.Identification.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "No email provided")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "No password provided")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password mismatch")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string ReturnUrl { get; set; }
    }
}
