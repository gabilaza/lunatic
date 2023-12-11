
using Lunatic.Application.Features.CommentEmotes.Payload;


namespace Lunatic.Application.Features.CommentEmotes.Queries.GetAll {
    public class GetAllCommentEmotesQueryResponse {
        public List<CommentEmoteDto> CommentEmotes { get; set; } = default!;
    }
}
