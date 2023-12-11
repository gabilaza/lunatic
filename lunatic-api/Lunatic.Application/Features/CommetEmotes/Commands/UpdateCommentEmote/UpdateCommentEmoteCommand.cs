
using Lunatic.Domain.Entities;
using MediatR;


namespace Lunatic.Application.Features.CommentEmotes.Commands.UpdateCommentEmote {
    public class UpdateCommentEmoteCommand : IRequest<UpdateCommentEmoteCommandResponse> {
        public Guid CommentEmoteId { get; set; } = default!;

        public Emote Emote { get; set; } = default!;
    }
}
