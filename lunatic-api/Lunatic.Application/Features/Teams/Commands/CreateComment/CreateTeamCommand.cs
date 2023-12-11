
using MediatR;


namespace Lunatic.Application.Features.Teams.Commands.CreateTeam {
    public class CreateTeamComand : IRequest<CreateTeamCommandResponse> {
        public Guid UserId { get; set; } = default!;

        public string Name { get; set; } = default!;
    }
}
