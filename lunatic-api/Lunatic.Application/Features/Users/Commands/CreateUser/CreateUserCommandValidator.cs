
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Users.Commands.CreateUser {
    internal class CreateUserCommandValidator : AbstractValidator<CreateUserCommand> {
        private readonly IUserRepository userRepository;

        public CreateUserCommandValidator(IUserRepository userRepository) {
            this.userRepository = userRepository;

            RuleFor(user => user.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(user => user.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(user => user.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.")
                .EmailAddress().WithMessage("{PropertyName} is not a valid email address.")
                .MustAsync(async (email, cancellationToken) => !await this.userRepository.ExistsByEmailAsync(email))
                .WithMessage("{PropertyName} exists already.");

            RuleFor(user => user.Username)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
                .MustAsync(async (username, cancellationToken) => !await this.userRepository.ExistsByUsernameAsync(username))
                .WithMessage("{PropertyName} exists already.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(user => user.Role)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .IsInEnum().WithMessage("{PropertyName} is not a valid role.");
        }
    }
}
