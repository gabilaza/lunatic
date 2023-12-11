
using MediatR;


namespace Lunatic.Application.Features.CommentEmotes.Commands.DeleteCommentEmote {
    public class DeleteCommentEmoteCommand : IRequest<DeleteCommentEmoteCommandResponse> {
        public Guid Id { get; set; }
    }
}
