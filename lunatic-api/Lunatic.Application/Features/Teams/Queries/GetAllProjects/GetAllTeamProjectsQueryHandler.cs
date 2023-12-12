
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Projects.Payload;
using MediatR;


namespace Lunatic.Application.Features.Teams.Queries.GetAllProjects {
    public class GetAllTeamProjectsQueryHandler : IRequestHandler<GetAllTeamProjectsQuery, GetAllTeamProjectsQueryResponse> {
        private readonly ITeamRepository teamRepository;

        private readonly IProjectRepository projectRepository;

        public GetAllTeamProjectsQueryHandler(ITeamRepository teamRepository, IProjectRepository projectRepository) {
            this.teamRepository = teamRepository;
            this.projectRepository = projectRepository;
        }

        public async Task<GetAllTeamProjectsQueryResponse> Handle(GetAllTeamProjectsQuery request, CancellationToken cancellationToken) {
            var teamResult = await this.teamRepository.FindByIdAsync(request.TeamId);
            if(!teamResult.IsSuccess) {
                return new GetAllTeamProjectsQueryResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Team not found" }
                };
            }

            GetAllTeamProjectsQueryResponse response = new GetAllTeamProjectsQueryResponse();
            var projectIds = teamResult.Value.ProjectIds;
            var taskProjects = projectIds.Select(async (projectId) => (await this.projectRepository.FindByIdAsync(projectId)).Value).ToList();
            var projects = await Task.WhenAll(taskProjects);

            response.Projects = projects.Select(project => new ProjectDto {
                ProjectId = project.ProjectId,
                TeamId = project.TeamId,

                Title = project.Title,
                Description = project.Description,

                TaskIds = project.TaskIds,
            }).ToList();
            response.TeamId = request.TeamId;
            return response;
        }
    }
}
