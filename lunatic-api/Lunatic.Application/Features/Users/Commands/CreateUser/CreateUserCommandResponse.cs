
using Lunatic.Application.Responses;


namespace Lunatic.Application.Features.Users.Commands.CreateUser {
    public class CreateUserCommandResponse : ResponseBase {
        public CreateUserCommandResponse() : base() { }
        public CreateUserDto User { get; set; } = default!;
    }
}
