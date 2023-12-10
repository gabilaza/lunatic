
using Lunatic.Application.Features.Tasks.Payload;


namespace Lunatic.Application.Features.Tasks.Queries.GetAll {
    public class GetAllTasksQueryResponse {
        public List<TaskDto> Tasks { get; set; } = default!;
    }
}
