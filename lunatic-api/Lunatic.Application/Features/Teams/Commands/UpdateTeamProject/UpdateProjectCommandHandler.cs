
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Projects.Payload;
using MediatR;


namespace Lunatic.Application.Features.Teams.Commands.UpdateTeamProject {
    public class UpdateTeamProjectCommandHandler : IRequestHandler<UpdateTeamProjectCommand, UpdateTeamProjectCommandResponse> {
        private readonly IProjectRepository projectRepository;

        public UpdateTeamProjectCommandHandler(IProjectRepository projectRepository) {
            this.projectRepository = projectRepository;
        }

        public async Task<UpdateTeamProjectCommandResponse> Handle(UpdateTeamProjectCommand request, CancellationToken cancellationToken) {
            var validator = new UpdateTeamProjectCommandValidator(this.projectRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new UpdateTeamProjectCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var projectResult = await this.projectRepository.FindByIdAsync(request.ProjectId);

            projectResult.Value.Update(request.Title, request.Description);

            var dbProjectResult = await this.projectRepository.UpdateAsync(projectResult.Value);

            return new UpdateTeamProjectCommandResponse {
                Success = true,
                Project = new ProjectDto {
                    ProjectId = dbProjectResult.Value.ProjectId,
                    TeamId = dbProjectResult.Value.TeamId,

                    Title = dbProjectResult.Value.Title,
                    Description = dbProjectResult.Value.Description,

                    TaskIds = dbProjectResult.Value.TaskIds,
                }
            };
        }
    }
}
