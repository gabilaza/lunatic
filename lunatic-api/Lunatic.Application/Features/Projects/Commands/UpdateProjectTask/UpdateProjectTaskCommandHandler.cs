
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Tasks.Payload;
using MediatR;


namespace Lunatic.Application.Features.Projects.Commands.UpdateProjectTask {
    public class UpdateProjectTaskCommandHandler : IRequestHandler<UpdateProjectTaskCommand, UpdateProjectTaskCommandResponse> {
        private readonly ITaskRepository taskRepository;

        public UpdateProjectTaskCommandHandler(ITaskRepository taskRepository) {
            this.taskRepository = taskRepository;
        }

        public async Task<UpdateProjectTaskCommandResponse> Handle(UpdateProjectTaskCommand request, CancellationToken cancellationToken) {
            var validator = new UpdateProjectTaskCommandValidator(this.taskRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new UpdateProjectTaskCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var taskResult = await this.taskRepository.FindByIdAsync(request.TaskId);

            taskResult.Value.Update(request.Title, request.Description, request.Priority, request.Status);

            var dbTaskResult = await this.taskRepository.UpdateAsync(taskResult.Value);

            return new UpdateProjectTaskCommandResponse {
                Success = true,
                Task = new TaskDto {
                    TaskId = dbTaskResult.Value.TaskId,
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
