
using Lunatic.Application.Responses;


namespace Lunatic.Application.Features.Tasks.Commands.CreateTask {
    public class CreateTaskCommandResponse : ResponseBase {
        public CreateTaskCommandResponse() : base() { }
        public CreateTaskDto Task { get; set; } = default!;
    }
}
