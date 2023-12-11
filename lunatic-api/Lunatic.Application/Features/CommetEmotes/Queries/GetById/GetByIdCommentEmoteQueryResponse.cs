
using Lunatic.Application.Responses;
using Lunatic.Application.Features.CommentEmotes.Payload;


namespace Lunatic.Application.Features.CommentEmotes.Queries.GetById {
    public class GetByIdCommentEmoteQueryResponse : ResponseBase {
        public GetByIdCommentEmoteQueryResponse() : base() {}

        public CommentEmoteDto CommentEmote { get; set; } = default!;
    }
}

