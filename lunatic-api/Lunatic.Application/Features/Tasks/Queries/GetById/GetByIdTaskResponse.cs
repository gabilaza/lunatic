using Lunatic.Application.Responses;


namespace Lunatic.Application.Features.Tasks.Queries.GetById {
    public class GetByIdTaskResponse : ResponseBase {
        public GetByIdTaskResponse() : base() {}

        public TaskDto Task { get; set; } = default!;
    }
}

