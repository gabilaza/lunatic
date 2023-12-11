
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Application.Features.Projects.Payload;
using MediatR;


namespace Lunatic.Application.Features.Teams.Commands.CreateTeamProject {
    public class CreateTeamProjectCommandHandler : IRequestHandler<CreateTeamProjectCommand, CreateTeamProjectCommandResponse> {
        private readonly IProjectRepository projectRepository;

        private readonly ITeamRepository teamRepository;

        private readonly IUserRepository userRepository;

        public CreateTeamProjectCommandHandler(IProjectRepository projectRepository, ITeamRepository teamRepository, IUserRepository userRepository) {
            this.projectRepository = projectRepository;
            this.teamRepository = teamRepository;
            this.userRepository = userRepository;
        }

        public async Task<CreateTeamProjectCommandResponse> Handle(CreateTeamProjectCommand request, CancellationToken cancellationToken) {
            var validator = new CreateTeamProjectCommandValidator(this.userRepository, this.teamRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new CreateTeamProjectCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var projectResult = Project.Create(request.UserId, request.TeamId, request.Title, request.Description);
            if(!projectResult.IsSuccess) {
                return new CreateTeamProjectCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { projectResult.Error }
                };
            }

            var team = (await this.teamRepository.FindByIdAsync(request.TeamId)).Value;
            team.AddProject(projectResult.Value);
            await this.teamRepository.UpdateAsync(team);

            await this.projectRepository.AddAsync(projectResult.Value);

            return new CreateTeamProjectCommandResponse {
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
