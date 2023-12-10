
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Comments.Payload;


namespace Lunatic.Application.Features.Comments.Commands.UpdateComment {
    public class UpdateCommentCommandResponse : ResponseBase {
        public UpdateCommentCommandResponse() : base() { }

        public CommentDto Comment { get; set; } = default!;
    }
}
