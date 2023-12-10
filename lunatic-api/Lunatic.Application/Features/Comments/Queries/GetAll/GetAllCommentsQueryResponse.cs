
using Lunatic.Application.Features.Comments.Payload;


namespace Lunatic.Application.Features.Comments.Queries.GetAll {
    public class GetAllCommentsQueryResponse {
        public List<CommentDto> Comments { get; set; } = default!;
    }
}
