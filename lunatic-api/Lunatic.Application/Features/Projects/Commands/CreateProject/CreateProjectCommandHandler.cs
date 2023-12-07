
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using MediatR;


namespace Lunatic.Application.Features.Projects.Commands.CreateProject {
    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, CreateProjectCommandResponse> {

        private readonly IProjectRepository projectRepository;

        public CreateProjectCommandHandler(IProjectRepository projectRepository) {
            this.projectRepository = projectRepository;
        }

        public async Task<CreateProjectCommandResponse> Handle(CreateProjectCommand request, CancellationToken cancellationToken) {
            var validator = new CreateProjectCommandValidator(projectRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new CreateProjectCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var project = Project.Create(request.CreatedByUserId, request.Title, request.Description);
            if(!project.IsSuccess) {
                return new CreateProjectCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { project.Error }
                };
            }

            await projectRepository.AddAsync(project.Value);

            return new CreateProjectCommandResponse {
                Success = true,
                Project = new ProjectDTO {
                    Id = project.Value.Id,
                    CreatedByUserId = project.Value.CreatedByUserId,
                    Title = project.Value.Title,
                    Description = project.Value.Description,
                    Tasks = project.Value.Tasks
                }
            };
        }
    }
}
