
// using Lunatic.Domain.Entities;


namespace Lunatic.Application.Features.Users.Queries {
    public class UserDto {
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        // public DateTime CreatedAt { get; set; }

        // public Role Role { get; set; }

        // public List<Team>? Teams { get; set; }
    }
}
