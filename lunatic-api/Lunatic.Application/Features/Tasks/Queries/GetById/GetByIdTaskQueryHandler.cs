
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Tasks.Payload;
using MediatR;


namespace Lunatic.Application.Features.Tasks.Queries.GetById {
    public class GetByIdTaskQueryHandler : IRequestHandler<GetByIdTaskQuery, GetByIdTaskQueryResponse> {
        private readonly ITaskRepository taskRepository;

        public GetByIdTaskQueryHandler(ITaskRepository taskRepository) {
            this.taskRepository = taskRepository;
        }

        public async Task<GetByIdTaskQueryResponse> Handle(GetByIdTaskQuery request, CancellationToken cancellationToken) {
            var taskResult = await this.taskRepository.FindByIdAsync(request.TaskId);
            if(!taskResult.IsSuccess) {
                return new GetByIdTaskQueryResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Task not found" }
                };
            }

            return new GetByIdTaskQueryResponse {
                Success = true,
                Task = new TaskDto {
                    TaskId = taskResult.Value.TaskId,
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
