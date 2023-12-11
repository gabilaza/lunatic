
using MediatR;


namespace Lunatic.Application.Features.Teams.Commands.UpdateTeamProject {
    public class UpdateTeamProjectCommand : IRequest<UpdateTeamProjectCommandResponse> {
        public Guid TeamId { get; set; } = default!;
        public Guid ProjectId { get; set; } = default!;

        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
    }
}
