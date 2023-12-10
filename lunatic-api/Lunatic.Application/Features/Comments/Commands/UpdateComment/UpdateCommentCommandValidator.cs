
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Comments.Commands.UpdateComment {
    internal class UpdateCommentCommandValidator : AbstractValidator<UpdateCommentCommand> {
        private readonly ICommentRepository commentRepository;

        public UpdateCommentCommandValidator(ICommentRepository commentRepository) {
            this.commentRepository = commentRepository;

            RuleFor(t => t.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(t => t.Content)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}
