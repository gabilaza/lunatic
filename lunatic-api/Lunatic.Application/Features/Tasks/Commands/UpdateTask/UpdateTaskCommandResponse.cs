
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Tasks.Payload;


namespace Lunatic.Application.Features.Tasks.Commands.UpdateTask {
    public class UpdateTaskCommandResponse : ResponseBase {
        public UpdateTaskCommandResponse() : base() { }
        public TaskDto Task { get; set; } = default!;
    }
}
