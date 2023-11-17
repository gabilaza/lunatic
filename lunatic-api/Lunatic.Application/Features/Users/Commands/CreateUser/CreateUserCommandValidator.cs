
using FluentValidation;


namespace Lunatic.Application.Features.Users.Commands.CreateUser {
    internal class CreateUserCommandValidator : AbstractValidator<CreateUserComand> {

        public CreateUserCommandValidator() {
            RuleFor(p => p.Role).NotNull();
            RuleFor(p => p.Username)
                .NotEmpty().WithMessage("Username is required")
                .NotNull()
                .MaximumLength(50).WithMessage("Username must not exceed 50 characters");
        }
    }
}
