
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Teams.Payload;


namespace Lunatic.Application.Features.Teams.Commands.CreateTeam {
    public class CreateTeamCommandResponse : ResponseBase {
        public CreateTeamCommandResponse() : base() { }

        public TeamDto Team { get; set; } = default!;
    }
}
