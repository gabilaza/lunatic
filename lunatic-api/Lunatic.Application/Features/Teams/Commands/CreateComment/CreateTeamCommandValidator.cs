
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Teams.Commands.CreateTeam {
    internal class CreateTeamCommandValidator : AbstractValidator<CreateTeamComand> {
        private readonly IUserRepository userRepository;

        public CreateTeamCommandValidator(IUserRepository userRepository) {
            this.userRepository = userRepository;

            RuleFor(t => t.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (userId, cancellationToken) => await this.userRepository.ExistsByIdAsync(userId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");
        }
    }
}
