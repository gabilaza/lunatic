
using MediatR;


namespace Lunatic.Application.Features.Tasks.Queries.GetByIdComment {
    public class GetByIdTaskCommentQuery : IRequest<GetByIdTaskCommentQueryResponse> {
        public Guid TaskId { get; set; } = default!;
        public Guid CommentId { get; set; } = default!;
    }
}
