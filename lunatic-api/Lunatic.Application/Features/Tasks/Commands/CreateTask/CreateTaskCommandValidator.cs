
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Tasks.Commands.CreateTask {
    internal class CreateTaskCommandValidator : AbstractValidator<CreateTaskComand> {
        private readonly IUserRepository userRepository;

        private readonly IProjectRepository projectRepository;

        public CreateTaskCommandValidator(IUserRepository userRepository, IProjectRepository projectRepository) {
            this.userRepository = userRepository;
            this.projectRepository = projectRepository;

            RuleFor(t => t.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (userId, cancellationToken) => await this.userRepository.ExistsByIdAsync(userId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(t => t.ProjectId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (projectId, cancellationToken) => await this.projectRepository.ExistsByIdAsync(projectId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(t => t.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(25).WithMessage("{PropertyName} must not exceed 25 characters.");

            RuleFor(t => t.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(t => t.Priority)
                .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
