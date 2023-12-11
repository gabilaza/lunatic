
using MediatR;


namespace Lunatic.Application.Features.CommentEmotes.Queries.GetById {
    public record GetByIdCommentEmoteQuery(Guid Id) : IRequest<GetByIdCommentEmoteQueryResponse>;
}
