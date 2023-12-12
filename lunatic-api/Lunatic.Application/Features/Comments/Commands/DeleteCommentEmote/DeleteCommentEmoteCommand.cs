
using MediatR;


namespace Lunatic.Application.Features.Comments.Commands.DeleteCommentEmote {
    public class DeleteCommentEmoteCommand : IRequest<DeleteCommentEmoteCommandResponse> {
        public Guid CommentId { get; set; }
        public Guid CommentEmoteId { get; set; }
    }
}
