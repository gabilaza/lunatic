
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Comments.Payload;


namespace Lunatic.Application.Features.Tasks.Commands.UpdateTaskComment {
    public class UpdateTaskCommentCommandResponse : ResponseBase {
        public UpdateTaskCommentCommandResponse() : base() { }

        public CommentDto Comment { get; set; } = default!;
    }
}
