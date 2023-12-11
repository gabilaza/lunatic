
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Projects.Payload;


namespace Lunatic.Application.Features.Teams.Commands.CreateTeamProject {
    public class CreateTeamProjectCommandResponse : ResponseBase {
        public CreateTeamProjectCommandResponse() : base() { }

        public ProjectDto Project { get; set; } = default!;
    }
}
