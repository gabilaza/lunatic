
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Tasks.Commands.UpdateTaskComment {
    internal class UpdateTaskCommentCommandValidator : AbstractValidator<UpdateTaskCommentCommand> {
        private readonly ICommentRepository commentRepository;

        public UpdateTaskCommentCommandValidator(ICommentRepository commentRepository) {
            this.commentRepository = commentRepository;

            RuleFor(request => request.CommentId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (commentId, cancellationToken) => await this.commentRepository.ExistsByIdAsync(commentId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(request => request.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
