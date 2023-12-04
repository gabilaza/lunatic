
using FluentValidation;
using Lunatic.Application.Persistence;


namespace Lunatic.Application.Features.Users.Commands.CreateUser {
	internal class CreateUserCommandValidator : AbstractValidator<CreateUserComand> {

		private readonly IUserRepository userRepository;

		public CreateUserCommandValidator(IUserRepository userRepository) {
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
				.EmailAddress().WithMessage("{PropertyName} is not a valid email address.")
				.Must(IsUniqueEmail).WithMessage("{PropertyName} already exists.");

			RuleFor(u => u.Username)
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.NotNull().WithMessage("{PropertyName} is required.")
				.MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.")
				.Must(IsUniqueUsername).WithMessage("{PropertyName} already exists.");

			RuleFor(u => u.Password)
				.NotEmpty().WithMessage("{PropertyName} is required.")
				.NotNull().WithMessage("{PropertyName} is required.")
				.MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

			RuleFor(p => p.Role).NotNull();
		}

		private bool IsUniqueUsername(string username) {
			var user = userRepository.FindByUsernameAsync(username);
			return !user.Result.IsSuccess;
		}

		private bool IsUniqueEmail(string email) {
			var user = userRepository.FindByEmailAsync(email);
			return !user.Result.IsSuccess;
		}
	}
}
