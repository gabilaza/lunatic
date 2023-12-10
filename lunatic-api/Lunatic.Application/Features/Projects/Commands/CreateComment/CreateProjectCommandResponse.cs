
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Projects.Payload;


namespace Lunatic.Application.Features.Projects.Commands.CreateProject {
    public class CreateProjectCommandResponse : ResponseBase {
        public CreateProjectCommandResponse() : base() { }

        public ProjectDto Project { get; set; } = default!;
    }
}
