
using MediatR;


namespace Lunatic.Application.Features.Teams.Queries.GetByIdProject {
    public class GetByIdTeamProjectQuery : IRequest<GetByIdTeamProjectQueryResponse> {
        public Guid TeamId { get; set; } = default!;
        public Guid ProjectId { get; set; } = default!;
    }
}
