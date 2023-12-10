
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Comments.Payload;


namespace Lunatic.Application.Features.Comments.Queries.GetById {
    public class GetByIdCommentResponse : ResponseBase {
        public GetByIdCommentResponse() : base() {}

        public CommentDto Comment { get; set; } = default!;
    }
}

