
using Lunatic.Application.Features.Users.Payload;


namespace Lunatic.Application.Features.Users.Queries.GetAll {
    public class GetAllUsersQueryResponse {
        public List<UserDto> Users { get; set; } = default!;
    }
}
