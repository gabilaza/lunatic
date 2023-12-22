
using MediatR;


namespace Lunatic.Application.Features.Users.Queries.GetAll {
    public class GetUsernameMatchesQuery : IRequest<GetUsernameMatchesQueryResponse> {
        public string Username { get; set; } = default!;
    }
}
