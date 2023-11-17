
using Lunatic.Domain.Entities;


namespace Lunatic.Application.Features.Users.Commands.UpdateUser {
    public class UpdateUserDto {
        public Guid Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        // public DateTime CreatedAt { get; set; }

        public Role Role { get; set; }
        public List<Team>? Teams { get; set; }
    }
}
