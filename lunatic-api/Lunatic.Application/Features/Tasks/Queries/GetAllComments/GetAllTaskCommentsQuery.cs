
using MediatR;


namespace Lunatic.Application.Features.Tasks.Queries.GetAllComments {
    public record GetAllTaskCommentsQuery(Guid TaskId) : IRequest<GetAllTaskCommentsQueryResponse>;
}
