
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Comments.Payload;


namespace Lunatic.Application.Features.Comments.Commands.CreateComment {
    public class CreateCommentCommandResponse : ResponseBase {
        public CreateCommentCommandResponse() : base() { }

        public CommentDto Comment { get; set; } = default!;
    }
}
