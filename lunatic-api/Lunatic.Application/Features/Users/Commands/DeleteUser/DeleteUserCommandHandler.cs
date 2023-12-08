
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Users.Commands.DeleteUser {
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, DeleteUserCommandResponse> {
        private readonly IUserRepository userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        public async Task<DeleteUserCommandResponse> Handle(DeleteUserCommand request, CancellationToken cancellationToken) {

            var userResult = await userRepository.FindByIdAsync(request.Id);
            if (!userResult.IsSuccess) {
                return new DeleteUserCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "User not found" }
                };
            }

            if (userResult.Value.IsDeleted) {
                return new DeleteUserCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "User already deleted" }
                };
            }

            userResult.Value.MarkAsDeleted();

            var updateResult = await userRepository.UpdateAsync(userResult.Value);

            if (!updateResult.IsSuccess) {
                return new DeleteUserCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { updateResult.Error }
                };

            }

            return new DeleteUserCommandResponse {
                Success = true
            };
        }
    }
}
