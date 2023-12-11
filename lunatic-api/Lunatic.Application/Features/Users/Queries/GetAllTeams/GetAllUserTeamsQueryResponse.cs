
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Teams.Payload;


namespace Lunatic.Application.Features.Users.Queries.GetAllTeams {
    public class GetAllUserTeamsQueryResponse : ResponseBase {
        public GetAllUserTeamsQueryResponse() : base() {}

        public Guid UserId { get; set; } = default!;
        public List<TeamDto> Teams { get; set; } = default!;
    }
}
