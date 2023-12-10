
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Teams.Commands.DeleteTeam {
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, DeleteTeamCommandResponse> {
        private readonly ITeamRepository teamRepository;

        public DeleteTeamCommandHandler(ITeamRepository teamRepository) {
            this.teamRepository = teamRepository;
        }

        public async Task<DeleteTeamCommandResponse> Handle(DeleteTeamCommand request, CancellationToken cancellationToken) {
            var result = await teamRepository.DeleteAsync(request.Id);

            if(!result.IsSuccess) {
                return new DeleteTeamCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { result.Error }
                };

            }
            return new DeleteTeamCommandResponse {
                Success = true
            };
        }
    }
}
