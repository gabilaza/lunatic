
using Lunatic.Application.Responses;
using Lunatic.Application.Features.CommentEmotes.Payload;


namespace Lunatic.Application.Features.CommentEmotes.Commands.CreateCommentEmote {
    public class CreateCommentEmoteCommandResponse : ResponseBase {
        public CreateCommentEmoteCommandResponse() : base() { }

        public CommentEmoteDto CommentEmote { get; set; } = default!;
    }
}
