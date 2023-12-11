
using Lunatic.Application.Responses;
using Lunatic.Application.Features.CommentEmotes.Payload;


namespace Lunatic.Application.Features.CommentEmotes.Commands.UpdateCommentEmote {
    public class UpdateCommentEmoteCommandResponse : ResponseBase {
        public UpdateCommentEmoteCommandResponse() : base() { }

        public CommentEmoteDto CommentEmote { get; set; } = default!;
    }
}
