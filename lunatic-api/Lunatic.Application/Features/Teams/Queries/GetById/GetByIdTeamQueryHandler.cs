
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Teams.Payload;
using MediatR;


namespace Lunatic.Application.Features.Teams.Queries.GetById {
    public class GetByIdTeamQueryHandler : IRequestHandler<GetByIdTeamQuery, GetByIdTeamQueryResponse> {
        private readonly ITeamRepository teamRepository;

        public GetByIdTeamQueryHandler(ITeamRepository teamRepository) {
            this.teamRepository = teamRepository;
        }

        public async Task<GetByIdTeamQueryResponse> Handle(GetByIdTeamQuery request, CancellationToken cancellationToken) {
            var team = await teamRepository.FindByIdAsync(request.Id);
            if(!team.IsSuccess) {
                return new GetByIdTeamQueryResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Team not found" }
                };
            }

            return new GetByIdTeamQueryResponse {
                Success = true,
                Team = new TeamDto {
                    Id = team.Value.Id,

                    Name = team.Value.Name,

                    MemberIds = team.Value.MemberIds,
                    ProjectIds = team.Value.ProjectIds,
                }
            };
        }
    }
}
