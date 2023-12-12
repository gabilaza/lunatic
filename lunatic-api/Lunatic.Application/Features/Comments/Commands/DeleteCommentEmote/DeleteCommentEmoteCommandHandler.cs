
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Comments.Commands.DeleteCommentEmote {
    public class DeleteCommentEmoteCommandHandler : IRequestHandler<DeleteCommentEmoteCommand, DeleteCommentEmoteCommandResponse> {
        private readonly ICommentRepository commentRepository;

        private readonly ICommentEmoteRepository commentEmoteRepository;

        public DeleteCommentEmoteCommandHandler(ICommentRepository commentRepository, ICommentEmoteRepository commentEmoteRepository) {
            this.commentRepository = commentRepository;
            this.commentEmoteRepository = commentEmoteRepository;
        }

        public async Task<DeleteCommentEmoteCommandResponse> Handle(DeleteCommentEmoteCommand request, CancellationToken cancellationToken) {
            var validator = new DeleteCommentEmoteCommandValidator(this.commentRepository, this.commentEmoteRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new DeleteCommentEmoteCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var comment = (await this.commentRepository.FindByIdAsync(request.CommentId)).Value;
            comment.RemoveEmote(request.CommentEmoteId);
            await this.commentRepository.UpdateAsync(comment);

            var result = await this.commentEmoteRepository.DeleteAsync(request.CommentEmoteId);

            if(!result.IsSuccess) {
                return new DeleteCommentEmoteCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { result.Error }
                };

            }
            return new DeleteCommentEmoteCommandResponse {
                Success = true
            };
        }
    }
}
