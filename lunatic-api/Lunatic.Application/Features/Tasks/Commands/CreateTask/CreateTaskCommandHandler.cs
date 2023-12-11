
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Tasks.Payload;
using Task = Lunatic.Domain.Entities.Task;
using MediatR;


namespace Lunatic.Application.Features.Tasks.Commands.CreateTask {
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskCommand, CreateTaskCommandResponse> {
        private readonly ITaskRepository taskRepository;

        private readonly IProjectRepository projectRepository;

        private readonly IUserRepository userRepository;

        public CreateTaskCommandHandler(ITaskRepository taskRepository, IProjectRepository projectRepository, IUserRepository userRepository) {
            this.taskRepository = taskRepository;
            this.projectRepository = projectRepository;
            this.userRepository = userRepository;
        }

        public async Task<CreateTaskCommandResponse> Handle(CreateTaskCommand request, CancellationToken cancellationToken) {
            var validator = new CreateTaskCommandValidator(this.userRepository, this.projectRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new CreateTaskCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var taskResult = Task.Create(request.UserId, request.ProjectId, request.Title, request.Description, request.Priority);
            if(!taskResult.IsSuccess) {
                return new CreateTaskCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { taskResult.Error }
                };
            }

            await this.taskRepository.AddAsync(taskResult.Value);

            return new CreateTaskCommandResponse {
                Success = true,
                Task = new TaskDto {
                    Id = taskResult.Value.Id,
                    ProjectId = taskResult.Value.ProjectId,

                    Title = taskResult.Value.Title,
                    Description = taskResult.Value.Description,
                    Priority = taskResult.Value.Priority,
                    Status = taskResult.Value.Status,

                    Tags = taskResult.Value.Tags,
                    CommentIds = taskResult.Value.CommentIds,
                    AssigneeIds = taskResult.Value.AssigneeIds,

                    StartedDate = taskResult.Value.StartedDate,
                    EndedDate = taskResult.Value.EndedDate,
                }
            };
        }
    }
}
