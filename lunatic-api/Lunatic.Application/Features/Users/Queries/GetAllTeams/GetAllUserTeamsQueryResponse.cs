using Lunatic.Application.Features.Teams.Payload;
using Lunatic.Application.Responses;


namespace Lunatic.Application.Features.Users.Queries.GetAllTeams {
	public class GetAllUserTeamsQueryResponse : ResponseBase {
		public GetAllUserTeamsQueryResponse() : base() { }
		public List<TeamDto> Teams { get; set; } = default!;
	}
}
