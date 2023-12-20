
using MediatR;


namespace Lunatic.Application.Features.Tasks.Queries.GetByIdComment {
    public class GetByIdTaskCommentQuery : IRequest<GetByIdTaskCommentQueryResponse> {
        public Guid CommentId { get; set; } = default!;
    }
}
