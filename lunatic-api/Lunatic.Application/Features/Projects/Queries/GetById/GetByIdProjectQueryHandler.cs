
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Projects.Queries.GetById {
    public class GetByIdProjectQueryHandler : IRequestHandler<GetByIdProjectQuery, GetByIdProjectResponse> {

        private readonly IProjectRepository projectRepository;

        public GetByIdProjectQueryHandler(IProjectRepository projectRepository) {
            this.projectRepository = projectRepository;
        }

        public async Task<GetByIdProjectResponse> Handle(GetByIdProjectQuery request, CancellationToken cancellationToken) {
            var project = await projectRepository.FindByIdAsync(request.Id);

            if(!project.IsSuccess) {
                return new GetByIdProjectResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Project not found" }
                };
            }

            return new GetByIdProjectResponse {
                Success = true,
                Project = new ProjectDto {
                    Id = project.Value.Id,
                    CreatedByUserId = project.Value.CreatedByUserId,
                    Title = project.Value.Title,
                    Description = project.Value.Description,
                    TaskIds = project.Value.TaskIds
                }
            };
        }
    }
}
