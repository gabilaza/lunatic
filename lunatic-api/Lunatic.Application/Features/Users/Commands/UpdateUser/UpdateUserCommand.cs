
using Lunatic.Domain.Entities;
using MediatR;


namespace Lunatic.Application.Features.Users.Commands.UpdateUser {
    public class UpdateUserCommand : IRequest<UpdateUserCommandResponse> {
        public Guid Id { get; set; }
        public string Username { get; set; } = default!;
        public string Password { get; set; } = default!;
        public Role Role { get; set; }

    }
}
