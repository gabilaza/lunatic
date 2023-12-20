
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Tasks.Queries.GetByIdComment {
    internal class GetByIdTaskCommentQueryValidator : AbstractValidator<GetByIdTaskCommentQuery> {
        private readonly ICommentRepository commentRepository;

        public GetByIdTaskCommentQueryValidator(ICommentRepository commentRepository) {
            this.commentRepository = commentRepository;

            RuleFor(request => request.CommentId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (commentId, cancellationToken) => await this.commentRepository.ExistsByIdAsync(commentId))
                .WithMessage("{PropertyName} must exists.");
        }
    }
}

