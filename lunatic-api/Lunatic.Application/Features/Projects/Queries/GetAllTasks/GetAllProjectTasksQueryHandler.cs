
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Tasks.Payload;
using MediatR;


namespace Lunatic.Application.Features.Projects.Queries.GetAllTasks {
    public class GetAllProjectTasksQueryHandler : IRequestHandler<GetAllProjectTasksQuery, GetAllProjectTasksQueryResponse> {
        private readonly IProjectRepository projectRepository;

        private readonly ITaskRepository taskRepository;

        public GetAllProjectTasksQueryHandler(IProjectRepository projectRepository, ITaskRepository taskRepository) {
            this.projectRepository = projectRepository;
            this.taskRepository = taskRepository;
        }

        public async Task<GetAllProjectTasksQueryResponse> Handle(GetAllProjectTasksQuery request, CancellationToken cancellationToken) {
            var projectResult = await this.projectRepository.FindByIdAsync(request.ProjectId);
            if(!projectResult.IsSuccess) {
                return new GetAllProjectTasksQueryResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Project not found" }
                };
            }

            GetAllProjectTasksQueryResponse response = new GetAllProjectTasksQueryResponse();
            var taskIds = projectResult.Value.TaskIds;
            var taskTasks = taskIds.Select(async (taskId) => (await this.taskRepository.FindByIdAsync(taskId)).Value).ToList();
            var tasks = await Task.WhenAll(taskTasks);

            response.Tasks = tasks.Select(task => new TaskDto {
                TaskId = task.TaskId,
                ProjectId = task.ProjectId,

                Title = task.Title,
                Description = task.Description,
                Priority = task.Priority,
                Status = task.Status,

                Tags = task.Tags,
                CommentIds = task.CommentIds,
                AssigneeIds = task.AssigneeIds,

                StartedDate = task.StartedDate,
                EndedDate = task.EndedDate,
            }).ToList();
            return response;
        }
    }
}
