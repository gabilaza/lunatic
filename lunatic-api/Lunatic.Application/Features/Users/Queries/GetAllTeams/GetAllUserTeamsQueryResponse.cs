using Lunatic.Application.Features.Teams.Payload;


namespace Lunatic.Application.Features.Users.Queries.GetAllTeams {
	public class GetAllUserTeamsQueryResponse {
		public List<TeamDto> Teams { get; set; } = default!;
	}
}
