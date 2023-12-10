
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Users.Payload;


namespace Lunatic.Application.Features.Users.Commands.UpdateUser {
    public class UpdateUserCommandResponse : ResponseBase {
        public UpdateUserCommandResponse() : base() { }
        public UserDto User { get; set; } = default!;
    }
}
