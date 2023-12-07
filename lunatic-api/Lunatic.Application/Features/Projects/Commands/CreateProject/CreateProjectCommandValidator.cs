
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Projects.Commands.CreateProject {
    internal class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand> {

        private readonly IProjectRepository projectRepository;

        public CreateProjectCommandValidator(IProjectRepository projectRepository) {
            this.projectRepository = projectRepository;

            RuleFor(p => p.CreatedByUserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(p => p.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(u => u.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(256).WithMessage("{PropertyName} must not exceed 256 characters.");

        }
    }
}
