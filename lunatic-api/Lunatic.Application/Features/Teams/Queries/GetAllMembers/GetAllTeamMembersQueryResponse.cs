
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Users.Payload;


namespace Lunatic.Application.Features.Teams.Queries.GetAllMembers {
    public class GetAllTeamMembersQueryResponse : ResponseBase {
        public GetAllTeamMembersQueryResponse() : base() {}

        public List<UserDto> Members { get; set; } = default!;
    }
}
