
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Projects.Payload;
using MediatR;


namespace Lunatic.Application.Features.Projects.Commands.UpdateProject {
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, UpdateProjectCommandResponse> {
        private readonly IProjectRepository projectRepository;

        public UpdateProjectCommandHandler(IProjectRepository projectRepository) {
            this.projectRepository = projectRepository;
        }

        public async Task<UpdateProjectCommandResponse> Handle(UpdateProjectCommand request, CancellationToken cancellationToken) {
            var validator = new UpdateProjectCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new UpdateProjectCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var projectResult = await this.projectRepository.FindByIdAsync(request.Id);
            if(!projectResult.IsSuccess) {
                return new UpdateProjectCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Project not found" }
                };
            }

            projectResult.Value.Update(request.Title, request.Description);

            var dbProjectResult = await this.projectRepository.UpdateAsync(projectResult.Value);

            return new UpdateProjectCommandResponse {
                Success = true,
                Project = new ProjectDto {
                    Id = dbProjectResult.Value.Id,
                    TeamId = dbProjectResult.Value.TeamId,

                    Title = dbProjectResult.Value.Title,
                    Description = dbProjectResult.Value.Description,

                    TaskIds = dbProjectResult.Value.TaskIds,
                }
            };
        }
    }
}
