
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Projects.Payload;


namespace Lunatic.Application.Features.Teams.Commands.UpdateTeamProject {
    public class UpdateTeamProjectCommandResponse : ResponseBase {
        public UpdateTeamProjectCommandResponse() : base() { }

        public ProjectDto Project { get; set; } = default!;
    }
}
