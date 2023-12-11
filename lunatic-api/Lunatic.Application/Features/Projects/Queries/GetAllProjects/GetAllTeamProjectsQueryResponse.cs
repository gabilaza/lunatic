
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Projects.Payload;


namespace Lunatic.Application.Features.Teams.Queries.GetAllProjects {
    public class GetAllTeamProjectsQueryResponse : ResponseBase {
        public GetAllTeamProjectsQueryResponse() : base() {}

        public Guid TeamId { get; set; } = default!;
        public List<ProjectDto> Projects { get; set; } = default!;
    }
}
