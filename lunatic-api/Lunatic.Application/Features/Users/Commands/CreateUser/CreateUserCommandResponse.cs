
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Users.Payload;


namespace Lunatic.Application.Features.Users.Commands.CreateUser {
    public class CreateUserCommandResponse : ResponseBase {
        public CreateUserCommandResponse() : base() { }
        public UserDto User { get; set; } = default!;
    }
}
