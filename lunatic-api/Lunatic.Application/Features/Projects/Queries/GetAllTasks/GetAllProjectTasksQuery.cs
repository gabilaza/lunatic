
using MediatR;


namespace Lunatic.Application.Features.Projects.Queries.GetAllTasks {
    public record GetAllProjectTasksQuery(Guid ProjectId) : IRequest<GetAllProjectTasksQueryResponse>;
}
