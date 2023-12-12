
using Lunatic.Domain.Entities;
using MediatR;


namespace Lunatic.Application.Features.Comments.Commands.CreateCommentEmote {
    public class CreateCommentEmoteCommand : IRequest<CreateCommentEmoteCommandResponse> {
        public Guid UserId { get; set; } = default!;
        public Guid CommentId { get; set; } = default!;

        public Emote Emote { get; set; } = default!;
    }
}
