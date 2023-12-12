
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Tasks.Payload;


namespace Lunatic.Application.Features.Projects.Queries.GetByIdTask {
    public class GetByIdProjectTaskQueryResponse : ResponseBase {
        public GetByIdProjectTaskQueryResponse() : base() {}

        public Guid ProjectId { get; set; } = default!;
        public TaskDto Task { get; set; } = default!;
    }
}
