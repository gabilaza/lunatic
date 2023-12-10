
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Comments.Commands.CreateComment {
    internal class CreateCommentCommandValidator : AbstractValidator<CreateCommentComand> {
        private readonly ICommentRepository commentRepository;

        public CreateCommentCommandValidator(ICommentRepository commentRepository) {
            this.commentRepository = commentRepository;

            RuleFor(t => t.UserId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(t => t.TaskId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(t => t.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
