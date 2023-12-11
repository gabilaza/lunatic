
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.CommentEmotes.Payload;
using MediatR;


namespace Lunatic.Application.Features.CommentEmotes.Queries.GetAll {
    public class GetAllCommentEmotesQueryHandler : IRequestHandler<GetAllCommentEmotesQuery, GetAllCommentEmotesQueryResponse> {
        private readonly ICommentEmoteRepository commentEmoteRepository;

        public GetAllCommentEmotesQueryHandler(ICommentEmoteRepository commentEmoteRepository) {
            this.commentEmoteRepository = commentEmoteRepository;
        }

        public async Task<GetAllCommentEmotesQueryResponse> Handle(GetAllCommentEmotesQuery request, CancellationToken cancellationToken) {
            GetAllCommentEmotesQueryResponse response = new GetAllCommentEmotesQueryResponse();
            var commentEmotesResult = await this.commentEmoteRepository.GetAllAsync();

            if(commentEmotesResult.IsSuccess) {
                response.CommentEmotes = commentEmotesResult.Value.Select(ce => new CommentEmoteDto {
                    Id = ce.Id,
                    UserId = ce.UserId,
                    CommentId = ce.CommentId,

                    Emote = ce.Emote,
                }).ToList();
            }
            return response;
        }
    }
}
