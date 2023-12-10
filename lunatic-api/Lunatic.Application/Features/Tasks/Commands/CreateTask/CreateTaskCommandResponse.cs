
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Tasks.Payload;


namespace Lunatic.Application.Features.Tasks.Commands.CreateTask {
    public class CreateTaskCommandResponse : ResponseBase {
        public CreateTaskCommandResponse() : base() { }
        public TaskDto Task { get; set; } = default!;
    }
}
