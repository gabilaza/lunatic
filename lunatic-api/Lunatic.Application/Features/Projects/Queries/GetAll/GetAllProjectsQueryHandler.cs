
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Projects.Payload;
using MediatR;


namespace Lunatic.Application.Features.Projects.Queries.GetAll {
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, GetAllProjectsQueryResponse> {
        private readonly IProjectRepository projectRepository;

        public GetAllProjectsQueryHandler(IProjectRepository projectRepository) {
            this.projectRepository = projectRepository;
        }

        public async Task<GetAllProjectsQueryResponse> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken) {
            GetAllProjectsQueryResponse response = new GetAllProjectsQueryResponse();
            var projects = await this.projectRepository.GetAllAsync();

            if(projects.IsSuccess) {
                response.Projects = projects.Value.Select(project => new ProjectDto {
                    ProjectId = project.ProjectId,
                    TeamId = project.TeamId,

                    Title = project.Title,
                    Description = project.Description,

                    TaskIds = project.TaskIds,
                }).ToList();
            }
            return response;
        }
    }
}
