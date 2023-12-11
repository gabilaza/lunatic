
using FluentValidation;


namespace Lunatic.Application.Features.Comments.Commands.UpdateComment {
    internal class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand> {
        public UpdateCommentCommandValidator() {
            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(t => t.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MaximumLength(100).WithMessage("{PropertyName} must not exceed 100 characters.");
        }
    }
}
