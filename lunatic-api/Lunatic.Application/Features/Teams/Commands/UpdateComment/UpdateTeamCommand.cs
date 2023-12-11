
using MediatR;


namespace Lunatic.Application.Features.Teams.Commands.UpdateTeam {
    public class UpdateTeamCommand : IRequest<UpdateTeamCommandResponse> {
        public Guid Id { get; set; } = default!;

        public string Name { get; set; } = default!;
    }
}
