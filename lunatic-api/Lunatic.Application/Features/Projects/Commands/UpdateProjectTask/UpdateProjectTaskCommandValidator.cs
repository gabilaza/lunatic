
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Projects.Commands.UpdateProjectTask {
    internal class UpdateProjectTaskCommandValidator : AbstractValidator<UpdateProjectTaskCommand> {
        private readonly IProjectRepository projectRepository;

        private readonly ITaskRepository taskRepository;

        public UpdateProjectTaskCommandValidator(IProjectRepository projectRepository, ITaskRepository taskRepository) {
            this.projectRepository = projectRepository;
            this.taskRepository = taskRepository;

            RuleFor(request => request.TaskId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (taskId, cancellationToken) => await this.taskRepository.ExistsByIdAsync(taskId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(request => request.ProjectId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (projectId, cancellationToken) => await this.projectRepository.ExistsByIdAsync(projectId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(request => request.Title)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(25).WithMessage("{PropertyName} must not exceed 25 characters.");

            RuleFor(request => request.Description)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(request => request.Priority)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .IsInEnum().WithMessage("{PropertyName} is not a valid priority.");

            RuleFor(request => request.Status)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .IsInEnum().WithMessage("{PropertyName} is not a valid status.");

            RuleFor(request => new {request.ProjectId, request.TaskId})
                .MustAsync(async (req, cancellationToken) => {
                        var project = (await this.projectRepository.FindByIdAsync(req.ProjectId)).Value;
                        return project.TaskIds.Contains(req.TaskId);})
                .WithMessage("Project must include TaskId.");
        }
    }
}
