﻿
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
            var validator = new UpdateTaskCommandValidator(taskRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new UpdateTaskCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var taskResult = await taskRepository.FindByIdAsync(request.Id);
            if(!taskResult.IsSuccess) {
                return new UpdateTaskCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Task not found" }
                };
            }

            taskResult.Value.Update(request.Title, request.Description, request.Priority, request.Status);

            var dbTask = await taskRepository.UpdateAsync(taskResult.Value);

            return new UpdateTaskCommandResponse {
                Success = true,
                Task = new TaskDto {
                    Id = dbTask.Value.Id,
                    Project = dbTask.Value.Project,

                    Title = dbTask.Value.Title,
                    Description = dbTask.Value.Description,
                    Priority = dbTask.Value.Priority,
                    Status = dbTask.Value.Status,

                    Tags = dbTask.Value.Tags,
                    Comments = dbTask.Value.Comments,
                    Assignees = dbTask.Value.Assignees,

                    StartedDate = dbTask.Value.StartedDate,
                    EndedDate = dbTask.Value.EndedDate,
                }
            };
        }
    }
}
