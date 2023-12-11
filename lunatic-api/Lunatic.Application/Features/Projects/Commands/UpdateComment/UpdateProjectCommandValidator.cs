
using FluentValidation;


namespace Lunatic.Application.Features.Projects.Commands.UpdateProject {
    internal class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand> {
        public UpdateProjectCommandValidator() {
            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(25).WithMessage("{PropertyName} must not exceed 25 characters.");

            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
