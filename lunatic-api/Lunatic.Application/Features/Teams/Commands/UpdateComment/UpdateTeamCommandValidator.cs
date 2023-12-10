
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Teams.Commands.UpdateTeam {
    internal class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand> {
        private readonly ITeamRepository commentRepository;

        public UpdateTeamCommandValidator(ITeamRepository commentRepository) {
            this.commentRepository = commentRepository;

            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(t => t.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");
        }
    }
}
