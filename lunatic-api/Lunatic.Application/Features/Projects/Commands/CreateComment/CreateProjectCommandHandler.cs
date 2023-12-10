
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Application.Features.Projects.Payload;
using MediatR;


namespace Lunatic.Application.Features.Projects.Commands.CreateProject {
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectComand, CreateProjectCommandResponse> {
        private readonly IProjectRepository projectRepository;

        private readonly ITeamRepository teamRepository;

        private readonly IUserRepository userRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository, ITeamRepository teamRepository, IUserRepository userRepository) {
            this.projectRepository = projectRepository;
            this.teamRepository = teamRepository;
            this.userRepository = userRepository;
        }

        public async Task<CreateProjectCommandResponse> Handle(CreateProjectComand request, CancellationToken cancellationToken) {
            var validator = new CreateProjectCommandValidator(projectRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new CreateProjectCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var team = await this.teamRepository.FindByIdAsync(request.TeamId);
            if(!team.IsSuccess) {
                return new CreateProjectCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Task not found" }
                };
            }

            var user = await this.userRepository.FindByIdAsync(request.UserId);
            if(!user.IsSuccess) {
                return new CreateProjectCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "User not found" }
                };
            }

            var project = Project.Create(user.Value.Id, team.Value.Id, request.Title, request.Description);
            if(!project.IsSuccess) {
                return new CreateProjectCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { project.Error }
                };
            }

            await projectRepository.AddAsync(project.Value);

            return new CreateProjectCommandResponse {
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
