
using Lunatic.Application.Responses;


namespace Lunatic.Application.Features.Tasks.Commands.UpdateTask {
    public class UpdateTaskCommandResponse : ResponseBase {
        public UpdateTaskCommandResponse() : base() { }
        public UpdateTaskDto Task { get; set; } = default!;
    }
}
