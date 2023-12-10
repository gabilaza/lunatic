
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Projects.Payload;
using MediatR;


namespace Lunatic.Application.Features.Projects.Queries.GetById {
    public class GetByIdProjectQueryHandler : IRequestHandler<GetByIdProjectQuery, GetByIdProjectQueryResponse> {
        private readonly IProjectRepository projectRepository;

        public GetByIdProjectQueryHandler(IProjectRepository projectRepository) {
            this.projectRepository = projectRepository;
        }

        public async Task<GetByIdProjectQueryResponse> Handle(GetByIdProjectQuery request, CancellationToken cancellationToken) {
            var project = await projectRepository.FindByIdAsync(request.Id);
            if(!project.IsSuccess) {
                return new GetByIdProjectQueryResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Project not found" }
                };
            }

            return new GetByIdProjectQueryResponse {
                Success = true,
                Project = new ProjectDto {
                    Id = project.Value.Id,
                    TeamId = project.Value.TeamId,

                    Title = project.Value.Title,
                    Description = project.Value.Description,

                    TaskIds = project.Value.TaskIds,
                }
            };
        }
    }
}
