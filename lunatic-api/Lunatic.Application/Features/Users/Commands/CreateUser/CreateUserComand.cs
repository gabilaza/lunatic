
using Lunatic.Domain.Entities;
using MediatR;


namespace Lunatic.Application.Features.Users.Commands.CreateUser {
    public class CreateUserComand : IRequest<CreateUserCommandResponse> {
        public string Username { get; private set; } = default!;
        public string Password { get; private set; } = default!;
        public Role Role { get; private set; }
    }
}
