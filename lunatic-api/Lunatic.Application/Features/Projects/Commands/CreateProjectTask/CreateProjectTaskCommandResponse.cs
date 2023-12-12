
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Tasks.Payload;


namespace Lunatic.Application.Features.Projects.Commands.CreateProjectTask {
    public class CreateProjectTaskCommandResponse : ResponseBase {
        public CreateProjectTaskCommandResponse() : base() { }

        public TaskDto Task { get; set; } = default!;
    }
}
