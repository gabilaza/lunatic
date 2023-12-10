
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Teams.Commands.CreateTeam {
    internal class CreateTeamCommandValidator : AbstractValidator<CreateTeamComand> {
        private readonly ITeamRepository teamRepository;

        public CreateTeamCommandValidator(ITeamRepository teamRepository) {
            this.teamRepository = teamRepository;

            RuleFor(t => t.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");
        }
    }
}
