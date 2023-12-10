
using Lunatic.Domain.Entities;


namespace Lunatic.Application.Features.Users.Payload {
    public class UserDto {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public Role Role { get; set; } = default!;

        public ICollection<Team> Teams { get; set; } = default!;
    }
}
