
using MediatR;


namespace Lunatic.Application.Features.Comments.Queries.GetById {
    public record GetByIdCommentQuery(Guid CommentId) : IRequest<GetByIdCommentQueryResponse>;
}
