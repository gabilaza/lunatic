
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Tasks.Payload;


namespace Lunatic.Application.Features.Projects.Queries.GetAllTasks {
    public class GetAllProjectTasksQueryResponse : ResponseBase {
        public GetAllProjectTasksQueryResponse() : base() {}

        public List<TaskDto> Tasks { get; set; } = default!;
    }
}
