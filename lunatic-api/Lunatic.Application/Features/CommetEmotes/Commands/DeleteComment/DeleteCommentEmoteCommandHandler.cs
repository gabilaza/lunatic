
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.CommentEmotes.Commands.DeleteCommentEmote {
    public class DeleteCommentEmoteCommandHandler : IRequestHandler<DeleteCommentEmoteCommand, DeleteCommentEmoteCommandResponse> {
        private readonly ICommentEmoteRepository commentEmoteRepository;

        public DeleteCommentEmoteCommandHandler(ICommentEmoteRepository commentEmoteRepository) {
            this.commentEmoteRepository = commentEmoteRepository;
        }

        public async Task<DeleteCommentEmoteCommandResponse> Handle(DeleteCommentEmoteCommand request, CancellationToken cancellationToken) {
            var result = await this.commentEmoteRepository.DeleteAsync(request.Id);

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
