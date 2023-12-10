
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
            var task = await taskRepository.FindByIdAsync(request.Id);
            if(!task.IsSuccess) {
                return new GetByIdTaskQueryResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Task not found" }
                };
            }

            return new GetByIdTaskQueryResponse {
                Success = true,
                Task = new TaskDto {
                    Id = task.Value.Id,
                    ProjectId = task.Value.ProjectId,

                    Title = task.Value.Title,
                    Description = task.Value.Description,
                    Priority = task.Value.Priority,
                    Status = task.Value.Status,

                    Tags = task.Value.Tags,
                    CommentIds = task.Value.CommentIds,
                    AssigneeIds = task.Value.AssigneeIds,

                    StartedDate = task.Value.StartedDate,
                    EndedDate = task.Value.EndedDate,
                }
            };
        }
    }
}
