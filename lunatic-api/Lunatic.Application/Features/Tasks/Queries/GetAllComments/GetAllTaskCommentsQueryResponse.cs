
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Comments.Payload;


namespace Lunatic.Application.Features.Tasks.Queries.GetAllComments {
    public class GetAllTaskCommentsQueryResponse : ResponseBase {
        public GetAllTaskCommentsQueryResponse() : base() {}

        public List<CommentDto> Comments { get; set; } = default!;
    }
}
