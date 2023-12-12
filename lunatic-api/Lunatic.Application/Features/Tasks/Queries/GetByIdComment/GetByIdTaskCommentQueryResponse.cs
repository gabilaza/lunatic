
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Comments.Payload;


namespace Lunatic.Application.Features.Tasks.Queries.GetByIdComment {
    public class GetByIdTaskCommentQueryResponse : ResponseBase {
        public GetByIdTaskCommentQueryResponse() : base() {}

        public CommentDto Comment { get; set; } = default!;
    }
}
