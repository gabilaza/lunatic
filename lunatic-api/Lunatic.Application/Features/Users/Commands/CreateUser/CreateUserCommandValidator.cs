
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Users.Commands.CreateUser {
    internal class CreateUserCommandValidator : AbstractValidator<CreateUserComand> {

        private readonly IUserRepository userRepository;

        public CreateUserCommandValidator(IUserRepository userRepository) {
            this.userRepository = userRepository;

            RuleFor(p => p.Role).NotNull();

            RuleFor(p => p.FirstName)
                .NotEmpty().WithMessage("First name is required")
                .NotNull()
                .MaximumLength(20).WithMessage("First name must not exceed 50 characters");

            RuleFor(p => p.LastName)
                .NotEmpty().WithMessage("LastName name is required")
                .NotNull()
                .MaximumLength(20).WithMessage("Last name must not exceed 50 characters");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("Email is required")
                .NotNull()
                .MaximumLength(20).WithMessage("Username must not exceed 50 characters");

            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("Username is required")
                .NotNull()
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters")
                .Must(UniqueUsername).WithMessage("This username is already taken");

            RuleFor(p => p.Password)
                .NotEmpty().WithMessage("Password is required")
                .NotNull()
                .MaximumLength(30).WithMessage("Password must not exceed 30 characters");
        }

        private bool UniqueUsername(string username) {
            var task = this.userRepository.FindByUsernameAsync(username);
            task.Wait();
            var result = task.Result;
            return result.IsSuccess;
        }
    }
}
