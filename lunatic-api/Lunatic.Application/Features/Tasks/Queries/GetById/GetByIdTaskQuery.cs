
using MediatR;


namespace Lunatic.Application.Features.Tasks.Queries.GetById {
    public record GetByIdTaskQuery(Guid Id) : IRequest<GetByIdTaskResponse>;
}
