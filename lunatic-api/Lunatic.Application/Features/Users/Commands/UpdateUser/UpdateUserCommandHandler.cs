
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

            var user = await userRepository.FindByIdAsync(request.Id);
            if (!user.IsSuccess) {
                return new UpdateUserCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "User not found" }
                };
            }

            //trebuie schimbat constructorul sa ia id si creationdate
            //trebuie schimbat si guid.new din constructor in create sau nici atat.
            //sau folosim inca un constructor

            // user.Value.Username = request.Username;
            // user.Value.Password = request.Password;
            // user.Value.Role = request.Role;


            var dbUser = await userRepository.UpdateAsync(user.Value);

            return new UpdateUserCommandResponse {
                Success = false,
                User = new UpdateUserDto {
                    Id = dbUser.Value.Id,
                    FirstName = dbUser.Value.FirstName,
                    LastName = dbUser.Value.LastName,
                    Username = dbUser.Value.Username,
                    Password = dbUser.Value.Password,
                    Role = dbUser.Value.Role
                }
            };


        }
    }
}
