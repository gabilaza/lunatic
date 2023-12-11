
using FluentValidation;


namespace Lunatic.Application.Features.Teams.Commands.UpdateTeam {
    internal class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand> {
        public UpdateTeamCommandValidator() {
            RuleFor(team => team.TeamId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(team => team.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");
        }
    }
}
