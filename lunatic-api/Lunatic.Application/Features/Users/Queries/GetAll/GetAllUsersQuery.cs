
using MediatR;


namespace Lunatic.Application.Features.Users.Queries.GetAll {
    public class GetAllUsersQuery : IRequest<GetAllUsersQueryResponse> {
        public string Username { get; set; } = default!;
    }
}
