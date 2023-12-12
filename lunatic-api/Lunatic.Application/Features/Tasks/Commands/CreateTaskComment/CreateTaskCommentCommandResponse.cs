
using Lunatic.Application.Responses;
using Lunatic.Application.Features.Comments.Payload;


namespace Lunatic.Application.Features.Tasks.Commands.CreateTaskComment {
    public class CreateTaskCommentCommandResponse : ResponseBase {
        public CreateTaskCommentCommandResponse() : base() { }

        public CommentDto Comment { get; set; } = default!;
    }
}
