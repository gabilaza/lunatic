
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Teams.Payload;
using MediatR;


namespace Lunatic.Application.Features.Users.Queries.GetAllTeams {
    public class GetAllUserTeamsQueryHandler : IRequestHandler<GetAllUserTeamsQuery, GetAllUserTeamsQueryResponse> {
        private readonly IUserRepository userRepository;

        private readonly ITeamRepository teamRepository;

        public GetAllUserTeamsQueryHandler(IUserRepository userRepository, ITeamRepository teamRepository) {
            this.userRepository = userRepository;
            this.teamRepository = teamRepository;
        }

        public async Task<GetAllUserTeamsQueryResponse> Handle(GetAllUserTeamsQuery request, CancellationToken cancellationToken) {
            var userResult = await this.userRepository.FindByIdAsync(request.UserId);
            if(!userResult.IsSuccess) {
                return new GetAllUserTeamsQueryResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "User not found" }
                };
            }

            GetAllUserTeamsQueryResponse response = new GetAllUserTeamsQueryResponse();
            var teamIds = userResult.Value.TeamIds;
            var taskTeams = teamIds.Select(async (teamId) => (await this.teamRepository.FindByIdAsync(teamId)).Value).ToList();
            var teams = await Task.WhenAll(taskTeams);

            response.Teams = teams.Select(team => new TeamDto {
                TeamId = team.TeamId,

                Name = team.Name,

                MemberIds = team.MemberIds,
                ProjectIds = team.ProjectIds,
            }).ToList();
            return response;
        }
    }
}
