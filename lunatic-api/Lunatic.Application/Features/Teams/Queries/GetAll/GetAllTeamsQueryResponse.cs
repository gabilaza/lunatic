
using Lunatic.Application.Features.Teams.Payload;


namespace Lunatic.Application.Features.Teams.Queries.GetAll {
    public class GetAllTeamsQueryResponse {
        public List<TeamDto> Teams { get; set; } = default!;
    }
}
