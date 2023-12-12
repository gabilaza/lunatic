
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Tasks.Commands.UpdateTaskComment {
    internal class UpdateTaskCommentCommandValidator : AbstractValidator<UpdateTaskCommentCommand> {
        private readonly ITaskRepository taskRepository;

        private readonly ICommentRepository commentRepository;

        public UpdateTaskCommentCommandValidator(ITaskRepository taskRepository, ICommentRepository commentRepository) {
            this.taskRepository = taskRepository;
            this.commentRepository = commentRepository;

            RuleFor(request => request.CommentId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (commentId, cancellationToken) => await this.commentRepository.ExistsByIdAsync(commentId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(request => request.TaskId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (taskId, cancellationToken) => await this.taskRepository.ExistsByIdAsync(taskId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(request => request.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");

            RuleFor(request => new {request.TaskId, request.CommentId})
                .MustAsync(async (req, cancellationToken) => {
                        var task = (await this.taskRepository.FindByIdAsync(req.TaskId)).Value;
                        return task.CommentIds.Contains(req.CommentId);})
                .WithMessage("Task must include CommentId.");
        }
    }
}
