
using MediatR;


namespace Lunatic.Application.Features.Teams.Queries.GetByIdProject {
    public class GetByIdTeamProjectQuery : IRequest<GetByIdTeamProjectQueryResponse> {
        public Guid ProjectId { get; set; } = default!;
    }
}
