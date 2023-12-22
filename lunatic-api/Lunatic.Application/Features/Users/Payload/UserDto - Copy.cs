
using Lunatic.Domain.Entities;


namespace Lunatic.Application.Features.Users.Payload {
    public class UpdateUserDto {
        public Guid UserId { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Username { get; set; } = default!;
    }
}
