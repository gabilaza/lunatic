
using Lunatic.Application.Responses;
using Lunatic.Application.Features.CommentEmotes.Payload;


namespace Lunatic.Application.Features.Comments.Queries.GetAllEmotes {
    public class GetAllCommentEmotesQueryResponse : ResponseBase {
        public GetAllCommentEmotesQueryResponse() : base() {}

        public List<CommentEmoteDto> CommentEmotes { get; set; } = default!;
    }
}
