
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
                response.Projects = projects.Value.Select(p => new ProjectDto {
                    Id = p.Id,
                    CreatedByUserId = p.CreatedByUserId,
                    Title = p.Title,
                    Description = p.Description,
                    TaskIds = p.TaskIds
                }).ToList();
            }
            return response;
        }
    }
}
