
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
            var projectResult = await this.projectRepository.FindByIdAsync(request.ProjectId);
            if(!projectResult.IsSuccess) {
                return new GetByIdProjectQueryResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Project not found" }
                };
            }

            return new GetByIdProjectQueryResponse {
                Success = true,
                Project = new ProjectDto {
                    ProjectId = projectResult.Value.ProjectId,
                    TeamId = projectResult.Value.TeamId,

                    Title = projectResult.Value.Title,
                    Description = projectResult.Value.Description,

                    TaskIds = projectResult.Value.TaskIds,
                }
            };
        }
    }
}
