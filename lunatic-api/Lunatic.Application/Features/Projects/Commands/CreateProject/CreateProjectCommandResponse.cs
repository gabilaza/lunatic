using Lunatic.Application.Responses;


namespace Lunatic.Application.Features.Projects.Commands.CreateProject {
    public class CreateProjectCommandResponse : ResponseBase {
        public CreateProjectCommandResponse() : base() { }
        public ProjectDTO Project { get; set; } = default!;
    }
}
