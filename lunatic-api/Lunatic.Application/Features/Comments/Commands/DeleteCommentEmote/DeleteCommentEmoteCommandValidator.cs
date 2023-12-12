
using Lunatic.Application.Persistence;
using FluentValidation;


namespace Lunatic.Application.Features.Comments.Commands.DeleteCommentEmote {
    internal class DeleteCommentEmoteCommandValidator : AbstractValidator<DeleteCommentEmoteCommand> {
        private readonly ICommentRepository commentRepository;

        private readonly ICommentEmoteRepository commentEmoteRepository;

        public DeleteCommentEmoteCommandValidator(ICommentRepository commentRepository, ICommentEmoteRepository commentEmoteRepository) {
            this.commentRepository = commentRepository;
            this.commentEmoteRepository = commentEmoteRepository;

            RuleFor(request => request.CommentId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (commentId, cancellationToken) => await this.commentRepository.ExistsByIdAsync(commentId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(request => request.CommentEmoteId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} is required.")
                .MustAsync(async (commentEmoteId, cancellationToken) => await this.commentEmoteRepository.ExistsByIdAsync(commentEmoteId))
                .WithMessage("{PropertyName} must exists.");

            RuleFor(request => new {request.CommentId, request.CommentEmoteId})
                .MustAsync(async (req, cancellationToken) => {
                        var comment = (await this.commentRepository.FindByIdAsync(req.CommentId)).Value;
                        return comment.EmoteIds.Contains(req.CommentEmoteId);})
                .WithMessage("Comment must include EmoteId.");
        }
    }
}
