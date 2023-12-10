
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Tasks.Payload;
using Task = Lunatic.Domain.Entities.Task;
using MediatR;


namespace Lunatic.Application.Features.Tasks.Commands.CreateTask {
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskComand, CreateTaskCommandResponse> {
        private readonly ITaskRepository taskRepository;

        private readonly IProjectRepository projectRepository;

        private readonly IUserRepository userRepository;

        public CreateTaskCommandHandler(ITaskRepository taskRepository, IProjectRepository projectRepository, IUserRepository userRepository) {
            this.taskRepository = taskRepository;
            this.projectRepository = projectRepository;
            this.userRepository = userRepository;
        }

        public async Task<CreateTaskCommandResponse> Handle(CreateTaskComand request, CancellationToken cancellationToken) {
            var validator = new CreateTaskCommandValidator(taskRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new CreateTaskCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var project = await this.projectRepository.FindByIdAsync(request.ProjectId);
            if(!project.IsSuccess) {
                return new CreateTaskCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Project not found" }
                };
            }

            var user = await this.userRepository.FindByIdAsync(request.UserId);
            if(!user.IsSuccess) {
                return new CreateTaskCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "User not found" }
                };
            }

            var task = Task.Create(user.Value, project.Value, request.Title, request.Description, request.Priority);
            if(!task.IsSuccess) {
                return new CreateTaskCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { task.Error }
                };
            }

            await taskRepository.AddAsync(task.Value);

            return new CreateTaskCommandResponse {
                Success = true,
                Task = new TaskDto {
                    Id = task.Value.Id,
                    Project = task.Value.Project,

                    Title = task.Value.Title,
                    Description = task.Value.Description,
                    Priority = task.Value.Priority,
                    Status = task.Value.Status,

                    Tags = task.Value.Tags,
                    Comments = task.Value.Comments,
                    Assignees = task.Value.Assignees,

                    StartedDate = task.Value.StartedDate,
                    EndedDate = task.Value.EndedDate,
                }
            };
        }
    }
}
