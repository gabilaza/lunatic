using MediatR;


namespace Lunatic.Application.Features.Users.Queries.GetAllTeams {
	public record GetAllUserTeamsQuery(Guid UserId) : IRequest<GetAllUserTeamsQueryResponse>;
}
