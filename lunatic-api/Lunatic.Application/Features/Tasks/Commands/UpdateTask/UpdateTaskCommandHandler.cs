
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Tasks.Payload;
using MediatR;


namespace Lunatic.Application.Features.Tasks.Commands.UpdateTask {
    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, UpdateTaskCommandResponse> {
        private readonly ITaskRepository taskRepository;

        public UpdateTaskCommandHandler(ITaskRepository taskRepository) {
            this.taskRepository = taskRepository;
        }

        public async Task<UpdateTaskCommandResponse> Handle(UpdateTaskCommand request, CancellationToken cancellationToken) {
            var validator = new UpdateTaskCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new UpdateTaskCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var taskResult = await this.taskRepository.FindByIdAsync(request.Id);
            if(!taskResult.IsSuccess) {
                return new UpdateTaskCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Task not found" }
                };
            }

            taskResult.Value.Update(request.Title, request.Description, request.Priority, request.Status);

            var dbTaskResult = await this.taskRepository.UpdateAsync(taskResult.Value);

            return new UpdateTaskCommandResponse {
                Success = true,
                Task = new TaskDto {
                    Id = dbTaskResult.Value.Id,
                    ProjectId = dbTaskResult.Value.ProjectId,

                    Title = dbTaskResult.Value.Title,
                    Description = dbTaskResult.Value.Description,
                    Priority = dbTaskResult.Value.Priority,
                    Status = dbTaskResult.Value.Status,

                    Tags = dbTaskResult.Value.Tags,
                    CommentIds = dbTaskResult.Value.CommentIds,
                    AssigneeIds = dbTaskResult.Value.AssigneeIds,

                    StartedDate = dbTaskResult.Value.StartedDate,
                    EndedDate = dbTaskResult.Value.EndedDate,
                }
            };
        }
    }
}
