
using MediatR;


namespace Lunatic.Application.Features.Projects.Queries.GetById {
    public record GetByIdProjectQuery(Guid Id) : IRequest<GetByIdProjectQueryResponse>;
}
