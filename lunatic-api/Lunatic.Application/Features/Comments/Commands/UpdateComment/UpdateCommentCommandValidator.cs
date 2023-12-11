
using FluentValidation;


namespace Lunatic.Application.Features.Comments.Commands.UpdateComment {
    internal class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand> {
        public UpdateCommentCommandValidator() {
            RuleFor(commet => commet.CommentId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(commet => commet.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
