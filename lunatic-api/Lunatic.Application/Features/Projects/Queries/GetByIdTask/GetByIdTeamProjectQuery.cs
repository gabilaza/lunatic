
using MediatR;


namespace Lunatic.Application.Features.Projects.Queries.GetByIdTask {
    public class GetByIdProjectTaskQuery : IRequest<GetByIdProjectTaskQueryResponse> {
        public Guid ProjectId { get; set; } = default!;
        public Guid TaskId { get; set; } = default!;
    }
}
