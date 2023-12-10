
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Users.Commands.DeleteUser {
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserComand, DeleteUserCommandResponse> {
        private readonly IUserRepository userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        public async Task<DeleteUserCommandResponse> Handle(DeleteUserComand request, CancellationToken cancellationToken) {
            var result = await userRepository.DeleteAsync(request.Id);

            if(!result.IsSuccess) {
                return new DeleteUserCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { result.Error }
                };

            }
            return new DeleteUserCommandResponse {
                Success = true
            };
        }
    }
}
