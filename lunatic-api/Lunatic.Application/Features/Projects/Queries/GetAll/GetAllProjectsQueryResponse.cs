
using Lunatic.Application.Features.Projects.Payload;


namespace Lunatic.Application.Features.Projects.Queries.GetAll {
    public class GetAllProjectsQueryResponse {
        public List<ProjectDto> Projects { get; set; } = default!;
    }
}
