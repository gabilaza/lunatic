
using Lunatic.Application.Responses;


namespace Lunatic.Application.Features.Projects.Commands.UpdateProject {
    public class UpdateProjectCommandResponse : ResponseBase {
        public UpdateProjectCommandResponse() : base() { }
        public ProjectDTO Project { get; set; } = default!;
    }
}
