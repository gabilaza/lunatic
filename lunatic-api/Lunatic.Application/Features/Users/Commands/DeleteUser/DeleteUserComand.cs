
using MediatR;


namespace Lunatic.Application.Features.Users.Commands.DeleteUser {
    public class DeleteUserComand : IRequest<DeleteUserCommandResponse> {
        public Guid Id { get; set; }
    }
}
