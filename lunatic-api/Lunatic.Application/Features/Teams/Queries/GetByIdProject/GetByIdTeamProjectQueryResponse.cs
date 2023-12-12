
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Projects.Payload;


namespace Lunatic.Application.Features.Teams.Queries.GetByIdProject {
    public class GetByIdTeamProjectQueryResponse : ResponseBase {
        public GetByIdTeamProjectQueryResponse() : base() {}

        public ProjectDto Project { get; set; } = default!;
    }
}
