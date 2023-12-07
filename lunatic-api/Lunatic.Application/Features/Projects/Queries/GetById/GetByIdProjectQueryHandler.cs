
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Projects.Queries.GetById {
    public class GetByIdProjectQueryHandler : IRequestHandler<GetByIdProjectQuery, ProjectDTO> {

        private readonly IProjectRepository projectRepository;

        public GetByIdProjectQueryHandler(IProjectRepository projectRepository) {
            this.projectRepository = projectRepository;
        }

        public async Task<ProjectDTO> Handle(GetByIdProjectQuery request, CancellationToken cancellationToken) {

            var project = await projectRepository.FindByIdAsync(request.Id);

            if (project.IsSuccess) {
                return new ProjectDTO {
                    Id = project.Value.Id,
                    CreatedByUserId = project.Value.CreatedByUserId,
                    Title = project.Value.Title,
                    Description = project.Value.Description,
                    Tasks = project.Value.Tasks
                };
            }
            return new ProjectDTO();
        }
    }
}
