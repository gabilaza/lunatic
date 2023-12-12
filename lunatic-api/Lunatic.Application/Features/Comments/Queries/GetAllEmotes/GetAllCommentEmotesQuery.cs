
using MediatR;


namespace Lunatic.Application.Features.Comments.Queries.GetAllEmotes {
    public record GetAllCommentEmotesQuery(Guid CommentId) : IRequest<GetAllCommentEmotesQueryResponse>;
}
