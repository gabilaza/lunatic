// using Lunatic.Domain.Entities;

namespace Lunatic.Application.Features.Users.Commands.CreateUser {
    public class CreateUserDto {
        public Guid UserId { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        // public DateTime CreatedAt { get; set; }
        //
        // public Role Role { get; set; }
        //
        // public List<Team>? Teams { get; set; }
    }
}
