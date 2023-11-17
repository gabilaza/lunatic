
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Users.Commands.UpdateUser {
    internal class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand> {
        private readonly IUserRepository userRepository;

        public UpdateUserCommandValidator(IUserRepository userRepository) {
            this.userRepository = userRepository;

            RuleFor(u => u.FirstName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(u => u.LastName)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(u => u.Email)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(320).WithMessage("{PropertyName} must not exceed 100 characters.")
                .EmailAddress().WithMessage("{PropertyName} is not a valid email address.");
                // .MustAsync(async (email, cancellation) => {
                //     var user = await userRepository.FindByEmailAsync(email); //extensie!
                //     return user.IsSuccess;
                // }).WithMessage("{PropertyName} already exists.");

            RuleFor(u => u.Username)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");
                // .MustAsync(async (username, cancellation) => {
                //     var user = await userRepository.FindByUsernameAsync(username);
                //     return user.IsSuccess;
                // }).WithMessage("{PropertyName} already exists.");

            RuleFor(u => u.Password)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Role).NotNull();
        }
    }
}
