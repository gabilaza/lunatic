
using Lunatic.Application.Persistence;
using Task = Lunatic.Domain.Entities.Task;
using MediatR;


namespace Lunatic.Application.Features.Tasks.Commands.CreateTask {
    public class CreateTaskCommandHandler : IRequestHandler<CreateTaskComand, CreateTaskCommandResponse> {
        private readonly ITaskRepository taskRepository;

        public CreateTaskCommandHandler(ITaskRepository taskRepository) {
            this.taskRepository = taskRepository;
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

            var task = Task.Create(request.UserId, request.Title, request.Description, request.Priority);
            if(!task.IsSuccess) {
                return new CreateTaskCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { task.Error }
                };
            }

            await taskRepository.AddAsync(task.Value);

            return new CreateTaskCommandResponse {
                Success = true,
                Task = new CreateTaskDto {
                    Id = task.Value.Id,
                    Title = task.Value.Title,
                    Description = task.Value.Description,
                    Priority = task.Value.Priority,
                    Status = task.Value.Status,
                    Tags = task.Value.Tags,
                    CommentIds = task.Value.CommentIds,
                    UserAssignIds = task.Value.UserAssignIds,
                    StartedDate = task.Value.StartedDate,
                    EndedDate = task.Value.EndedDate,
                }
            };
        }
    }
}
