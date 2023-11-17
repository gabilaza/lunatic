
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using MediatR;


namespace Lunatic.Application.Features.Users.Commands.CreateUser {
    public class CreateUserCommandHandler : IRequestHandler<CreateUserComand, CreateUserCommandResponse> {

        private readonly IUserRepository userRepository;

        public CreateUserCommandHandler(IUserRepository userRepository) {
            this.userRepository = userRepository;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserComand request, CancellationToken cancellationToken) {
            var validator = new CreateUserCommandValidator(userRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new CreateUserCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var user = User.Create(request.FirstName, request.LastName, request.Email, request.Username, request.Password, request.Role);
            if(!user.IsSuccess) {
                return new CreateUserCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { user.Error }
                };
            }

            await userRepository.AddAsync(user.Value);

            return new CreateUserCommandResponse {
                Success = true,
                User = new CreateUserDto {
                    Id = user.Value.Id,
                    FirstName = user.Value.FirstName,
                    LastName = user.Value.LastName,
                    Email = user.Value.Email,
                    Username = user.Value.Username,
                    Password = user.Value.Password,
                    Role = user.Value.Role,
                    Teams = user.Value.Teams
                }
            };
        }
    }
}
