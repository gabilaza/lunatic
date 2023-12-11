
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Tasks.Payload;
using MediatR;


namespace Lunatic.Application.Features.Tasks.Queries.GetAll {
    public class GetAllTasksQueryHandler : IRequestHandler<GetAllTasksQuery, GetAllTasksQueryResponse> {
        private readonly ITaskRepository taskRepository;

        public GetAllTasksQueryHandler(ITaskRepository taskRepository) {
            this.taskRepository = taskRepository;
        }

        public async Task<GetAllTasksQueryResponse> Handle(GetAllTasksQuery request, CancellationToken cancellationToken) {
            GetAllTasksQueryResponse response = new GetAllTasksQueryResponse();
            var tasks = await this.taskRepository.GetAllAsync();

            if(tasks.IsSuccess) {
                response.Tasks = tasks.Value.Select(t => new TaskDto {
                    Id = t.Id,
                    ProjectId = t.ProjectId,

                    Title = t.Title,
                    Description = t.Description,
                    Priority = t.Priority,
                    Status = t.Status,

                    Tags = t.Tags,
                    CommentIds = t.CommentIds,
                    AssigneeIds = t.AssigneeIds,

                    StartedDate = t.StartedDate,
                    EndedDate = t.EndedDate,
                }).ToList();
            }
            return response;
        }
    }
}
