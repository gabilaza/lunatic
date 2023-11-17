
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using MediatR;


namespace Lunatic.Application.Features.Users.Commands.CreateUser {
    public class CreateUserCommandHandler : IRequestHandler<CreateUserComand, CreateUserCommandResponse> {

        private readonly IUserRepository repository;

        public CreateUserCommandHandler(IUserRepository repository) {
            this.repository = repository;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserComand request, CancellationToken cancellationToken) {
            var validator = new CreateUserCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new CreateUserCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var user = User.Create(request.Username, request.Password, request.Role);
            if(!user.IsSuccess) {
                return new CreateUserCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { user.Error }
                };
            }

            await repository.AddAsync(user.Value);

            return new CreateUserCommandResponse {
                Success = true,
                User = new CreateUserDto {
                    Username = user.Value.Username,
                    Password = user.Value.Password
                    // Role = user.Value.Role
                }
            };
        }
    }
}
