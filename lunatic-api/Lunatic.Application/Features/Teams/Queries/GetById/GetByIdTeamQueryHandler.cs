
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
            var teamResult = await this.teamRepository.FindByIdAsync(request.Id);
            if(!teamResult.IsSuccess) {
                return new GetByIdTeamQueryResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Team not found" }
                };
            }

            return new GetByIdTeamQueryResponse {
                Success = true,
                Team = new TeamDto {
                    Id = teamResult.Value.Id,

                    Name = teamResult.Value.Name,

                    MemberIds = teamResult.Value.MemberIds,
                    ProjectIds = teamResult.Value.ProjectIds,
                }
            };
        }
    }
}
