using System.ComponentModel.DataAnnotations;

namespace Lunatic.Application.Models.Identity
{
    public class RegistrationModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string? Username { get; set; }

        //[Required(ErrorMessage = "Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "LastName is required")]
        public string? LastName { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email is required")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string? Password { get; set; }
    }
}
