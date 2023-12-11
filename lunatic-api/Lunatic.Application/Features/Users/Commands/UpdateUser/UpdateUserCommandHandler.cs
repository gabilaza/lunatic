
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Users.Payload;
using MediatR;


namespace Lunatic.Application.Features.Users.Commands.UpdateUser {
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, UpdateUserCommandResponse> {
        private readonly IUserRepository userRepository;

        public UpdateUserCommandHandler(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        public async Task<UpdateUserCommandResponse> Handle(UpdateUserCommand request, CancellationToken cancellationToken) {
            var validator = new UpdateUserCommandValidator(this.userRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new UpdateUserCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var userResult = await this.userRepository.FindByIdAsync(request.Id);
            if(!userResult.IsSuccess) {
                return new UpdateUserCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "User not found" }
                };
            }

            userResult.Value.Update(request.FirstName, request.LastName, request.Email, request.Username, request.Password, request.Role);

            var dbUserResult = await this.userRepository.UpdateAsync(userResult.Value);

            return new UpdateUserCommandResponse {
                Success = true,
                User = new UserDto {
                    Id = dbUserResult.Value.Id,

                    FirstName = dbUserResult.Value.FirstName,
                    LastName = dbUserResult.Value.LastName,
                    Email = dbUserResult.Value.Email,
                    Username = dbUserResult.Value.Username,
                    Password = dbUserResult.Value.Password,
                    Role = dbUserResult.Value.Role,

                    TeamIds = dbUserResult.Value.TeamIds
                }
            };


        }
    }
}
