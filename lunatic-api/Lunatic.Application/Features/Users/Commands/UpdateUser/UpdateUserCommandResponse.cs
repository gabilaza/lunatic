
using Lunatic.Application.Responses;


namespace Lunatic.Application.Features.Users.Commands.UpdateUser {
    public class UpdateUserCommandResponse : ResponseBase {
        public UpdateUserCommandResponse() : base() { }
        public UpdateUserDto User { get; set; } = default!;
    }
}
