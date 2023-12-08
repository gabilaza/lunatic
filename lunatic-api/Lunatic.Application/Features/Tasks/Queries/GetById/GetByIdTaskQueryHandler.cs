
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Tasks.Queries.GetById {
    public class GetByIdTaskQueryHandler : IRequestHandler<GetByIdTaskQuery, GetByIdTaskResponse> {
        private readonly ITaskRepository taskRepository;

        public GetByIdTaskQueryHandler(ITaskRepository taskRepository) {
            this.taskRepository = taskRepository;
        }

        public async Task<GetByIdTaskResponse> Handle(GetByIdTaskQuery request, CancellationToken cancellationToken) {
            var task = await taskRepository.FindByIdAsync(request.Id);
            if(!task.IsSuccess) {
                return new GetByIdTaskResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Task not found" }
                };
            }

            return new GetByIdTaskResponse {
                Success = true,
                Task = new TaskDto {
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
