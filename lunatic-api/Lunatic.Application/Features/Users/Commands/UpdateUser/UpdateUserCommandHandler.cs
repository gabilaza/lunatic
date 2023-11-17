
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Users.Commands.UpdateUser {
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommandResponse> {
        private readonly IUserRepository userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken) {
            var response = new UpdateUserCommandResponse();
            var validator = new UpdateUserCommandValidator(userRepository);

            var validatorResult = await validator.ValidateAsync(request, cancellationToken);
            if(!validatorResult.IsValid) {
                return new UpdateUserCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var userResult = await userRepository.FindByIdAsync(request.Id);
            if(!userResult.IsSuccess) {
                return new UpdateUserCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "User not found" }
                };
            }

            userResult.Value.Update(request.FirstName, request.LastName, request.Email, request.Username, request.Password, request.Role, request.Teams);

            var dbUser = await userRepository.UpdateAsync(userResult.Value);

            return new UpdateUserCommandResponse {
                Success = false,
                User = new UpdateUserDto {
                    Id = dbUser.Value.Id,
                    FirstName = dbUser.Value.FirstName,
                    LastName = dbUser.Value.LastName,
                    Email = dbUser.Value.Email,
                    Username = dbUser.Value.Username,
                    Password = dbUser.Value.Password,
                    Role = dbUser.Value.Role,
                    Teams = dbUser.Value.Teams
                }
            };


        }
    }
}
