
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Tasks.Commands.CreateTaskComment {
    internal class CreateTaskCommentCommandValidator : AbstractValidator<CreateTaskCommentCommand> {
        private readonly IUserRepository userRepository;

        private readonly ITaskRepository taskRepository;

        public CreateTaskCommentCommandValidator(IUserRepository userRepository, ITaskRepository taskRepository) {
            this.userRepository = userRepository;
            this.taskRepository = taskRepository;

            RuleFor(request => request.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (userId, cancellationToken) => await this.userRepository.ExistsByIdAsync(userId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(request => request.TaskId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (taskId, cancellationToken) => await this.taskRepository.ExistsByIdAsync(taskId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(request => request.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(3000).WithMessage("{PropertyName} must not exceed 3000 characters.");
        }
    }
}
