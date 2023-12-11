
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.CommentEmotes.Payload;
using MediatR;


namespace Lunatic.Application.Features.CommentEmotes.Queries.GetById {
    public class GetByIdCommentEmoteQueryHandler : IRequestHandler<GetByIdCommentEmoteQuery, GetByIdCommentEmoteQueryResponse> {
        private readonly ICommentEmoteRepository commentEmoteRepository;

        public GetByIdCommentEmoteQueryHandler(ICommentEmoteRepository commentEmoteRepository) {
            this.commentEmoteRepository = commentEmoteRepository;
        }

        public async Task<GetByIdCommentEmoteQueryResponse> Handle(GetByIdCommentEmoteQuery request, CancellationToken cancellationToken) {
            var commentEmoteResult = await this.commentEmoteRepository.FindByIdAsync(request.Id);
            if(!commentEmoteResult.IsSuccess) {
                return new GetByIdCommentEmoteQueryResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Comment Emote not found" }
                };
            }

            return new GetByIdCommentEmoteQueryResponse {
                Success = true,
                CommentEmote = new CommentEmoteDto {
                    Id = commentEmoteResult.Value.Id,
                    UserId = commentEmoteResult.Value.UserId,
                    CommentId = commentEmoteResult.Value.CommentId,

                    Emote = commentEmoteResult.Value.Emote,
                }
            };
        }
    }
}
