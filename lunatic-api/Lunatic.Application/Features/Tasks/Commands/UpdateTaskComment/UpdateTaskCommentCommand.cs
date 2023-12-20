
using MediatR;


namespace Lunatic.Application.Features.Tasks.Commands.UpdateTaskComment {
    public class UpdateTaskCommentCommand : IRequest<UpdateTaskCommentCommandResponse> {
        public Guid CommentId { get; set; } = default!;

        public string Content { get; set; } = default!;
    }
}
