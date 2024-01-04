using System.ComponentModel.DataAnnotations;
using Lunatic.UI.ViewModels;

namespace Lunatic.UI.Dtos
{
    public class UserDto
    {
        public Guid UserId { get; set; } = default!;

        [Required(ErrorMessage = "First Name is required")]
        public string FirstName { get; set; } = default!;

        [Required(ErrorMessage = "Last Name is required")]
        public string LastName { get; set; } = default!;

        [Required(ErrorMessage = "A valid email address is required")]
        [EmailAddress(ErrorMessage = "A valid email address is required")]
        public string Email { get; set; } = default!;

        [Required(ErrorMessage = "Userame is required")]
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public Role Role { get; set; } = default!;
        public List<Guid>? TeamIds { get; set; }

        public override string ToString()
        {
            return $"UserDto(UserId: {UserId}, FirstName: {FirstName}, LastName: {LastName}, Email: {Email}, Username: {Username}, Password: {Password}, Role: {Role}, TeamIds: {string.Join(Environment.NewLine, TeamIds == null ? [] : TeamIds)})";
        }
    }
}