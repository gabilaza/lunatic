
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Projects.Queries.GetAll {
    public class GetAllProjectsQueryHandler : IRequestHandler<GetAllProjectsQuery, GetAllProjectsQueryResponse> {
        private readonly IProjectRepository projectRepository;

        public GetAllProjectsQueryHandler(IProjectRepository projectRepository) {
            this.projectRepository = projectRepository;
        }

        public async Task<GetAllProjectsQueryResponse> Handle(GetAllProjectsQuery request, CancellationToken cancellationToken) {
            GetAllProjectsQueryResponse response = new GetAllProjectsQueryResponse();
            var projects = await projectRepository.GetAllAsync();

            if(projects.IsSuccess) {
                response.Projects = projects.Value.Select(u => new ProjectDTO {
                    Id = u.Id,
                    CreatedByUserId = u.CreatedByUserId,
                    Title = u.Title,
                    Description = u.Description,
                    Tasks = u.Tasks
                }).ToList();
            }
            return response;
        }
    }
}
