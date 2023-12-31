
using Lunatic.Domain.Entities;
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.CommentEmotes.Payload;
using MediatR;


namespace Lunatic.Application.Features.Comments.Queries.GetAllEmotes {
    public class GetAllCommentEmotesQueryHandler : IRequestHandler<GetAllCommentEmotesQuery, GetAllCommentEmotesQueryResponse> {
        private readonly ICommentRepository commentRepository;

        private readonly ICommentEmoteRepository commentEmoteRepository;

        public GetAllCommentEmotesQueryHandler(ICommentRepository commentRepository, ICommentEmoteRepository commentEmoteRepository) {
            this.commentRepository = commentRepository;
            this.commentEmoteRepository = commentEmoteRepository;
        }

        public async Task<GetAllCommentEmotesQueryResponse> Handle(GetAllCommentEmotesQuery request, CancellationToken cancellationToken) {
            var commentResult = await this.commentRepository.FindByIdAsync(request.CommentId);
            if(!commentResult.IsSuccess) {
                return new GetAllCommentEmotesQueryResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Comment not found" }
                };
            }

            GetAllCommentEmotesQueryResponse response = new GetAllCommentEmotesQueryResponse();
            var emoteIds = commentResult.Value.EmoteIds;
            var emotes = new List<CommentEmote>();
            foreach (var emoteId in emoteIds) {
                var emote = (await this.commentEmoteRepository.FindByIdAsync(emoteId)).Value;
                emotes.Add(emote);
            }


            response.CommentEmotes = emotes.Select(commentEmote => new CommentEmoteDto {
                CommentEmoteId = commentEmote.CommentEmoteId,
                UserId = commentEmote.UserId,
                CommentId = commentEmote.CommentId,

                Emote = commentEmote.Emote,
            }).ToList();
            return response;
        }
    }
}
