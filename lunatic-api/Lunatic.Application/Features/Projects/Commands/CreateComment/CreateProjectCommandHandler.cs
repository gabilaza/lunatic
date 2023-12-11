
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
            var validator = new CreateProjectCommandValidator(this.userRepository, this.teamRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new CreateProjectCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var projectResult = Project.Create(request.UserId, request.TeamId, request.Title, request.Description);
            if(!projectResult.IsSuccess) {
                return new CreateProjectCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { projectResult.Error }
                };
            }

            await this.projectRepository.AddAsync(projectResult.Value);

            return new CreateProjectCommandResponse {
                Success = true,
                Project = new ProjectDto {
                    Id = projectResult.Value.Id,
                    TeamId = projectResult.Value.TeamId,

                    Title = projectResult.Value.Title,
                    Description = projectResult.Value.Description,

                    TaskIds = projectResult.Value.TaskIds,
                }
            };
        }
    }
}
