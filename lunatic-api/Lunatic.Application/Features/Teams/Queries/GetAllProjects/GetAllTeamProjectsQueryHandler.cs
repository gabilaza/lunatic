
using Lunatic.Domain.Entities;
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
            var projects = new List<Project>();
            foreach (var projectId in projectIds) {
                var project = (await this.projectRepository.FindByIdAsync(projectId)).Value;
                projects.Add(project);
            }

            response.Projects = projects.Select(project => new ProjectDto {
                ProjectId = project.ProjectId,
                TeamId = project.TeamId,

                Title = project.Title,
                Description = project.Description,

                TaskIds = project.TaskIds,
            }).ToList();
            return response;
        }
    }
}
