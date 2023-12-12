
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Teams.Payload;


namespace Lunatic.Application.Features.Teams.Commands.DeleteTeamMember {
    public class DeleteTeamMemberCommandResponse : ResponseBase {
        public DeleteTeamMemberCommandResponse() : base() { }

        public TeamDto Team { get; set; } = default!;
    }
}
