
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Tasks.Payload;


namespace Lunatic.Application.Features.Projects.Commands.UpdateProjectTask {
    public class UpdateProjectTaskCommandResponse : ResponseBase {
        public UpdateProjectTaskCommandResponse() : base() { }

        public TaskDto Task { get; set; } = default!;
    }
}
