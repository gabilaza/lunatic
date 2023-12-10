
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Comments.Payload;


namespace Lunatic.Application.Features.Comments.Queries.GetById {
    public class GetByIdCommentQueryResponse : ResponseBase {
        public GetByIdCommentQueryResponse() : base() {}

        public CommentDto Comment { get; set; } = default!;
    }
}

