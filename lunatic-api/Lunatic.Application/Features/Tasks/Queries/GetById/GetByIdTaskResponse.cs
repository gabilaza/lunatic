
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Tasks.Payload;


namespace Lunatic.Application.Features.Tasks.Queries.GetById {
    public class GetByIdTaskResponse : ResponseBase {
        public GetByIdTaskResponse() : base() {}

        public TaskDto Task { get; set; } = default!;
    }
}

