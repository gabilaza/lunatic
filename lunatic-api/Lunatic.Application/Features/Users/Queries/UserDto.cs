
using Lunatic.Domain.Entities;
using Lunatic.Domain.Models;


namespace Lunatic.Application.Features.Users.Queries
{
    public class UserDto {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        // public DateTime CreatedAt { get; set; }

        public Role Role { get; set; }

        public List<Team>? Teams { get; set; }
    }
}
