
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Projects.Payload;
using MediatR;


namespace Lunatic.Application.Features.Teams.Queries.GetByIdProject {
    public class GetByIdTeamProjectQueryHandler : IRequestHandler<GetByIdTeamProjectQuery, GetByIdTeamProjectQueryResponse> {
        private readonly IProjectRepository projectRepository;

        public GetByIdTeamProjectQueryHandler(IProjectRepository projectRepository) {
            this.projectRepository = projectRepository;
        }

        public async Task<GetByIdTeamProjectQueryResponse> Handle(GetByIdTeamProjectQuery request, CancellationToken cancellationToken) {
            var validator = new GetByIdTeamProjectQueryValidator(this.projectRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new GetByIdTeamProjectQueryResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var project = (await this.projectRepository.FindByIdAsync(request.ProjectId)).Value;

            return new GetByIdTeamProjectQueryResponse {
                Success = true,
                Project = new ProjectDto {
                    ProjectId = project.ProjectId,
                    TeamId = project.TeamId,

                    Title = project.Title,
                    Description = project.Description,

                    TaskIds = project.TaskIds,
                }
            };
        }
    }
}
