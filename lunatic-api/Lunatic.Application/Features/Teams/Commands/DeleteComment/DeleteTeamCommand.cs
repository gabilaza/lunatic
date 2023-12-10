
using MediatR;


namespace Lunatic.Application.Features.Teams.Commands.DeleteTeam {
    public class DeleteTeamCommand : IRequest<DeleteTeamCommandResponse> {
        public Guid Id { get; set; }
    }
}
