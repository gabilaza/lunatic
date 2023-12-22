
using Lunatic.Application.Features.Users.Payload;


namespace Lunatic.Application.Features.Users.Queries.GetAll {
    public class GetUsernameMatchesQueryResponse {
        public List<UserDto> Users { get; set; } = default!;
    }
}
