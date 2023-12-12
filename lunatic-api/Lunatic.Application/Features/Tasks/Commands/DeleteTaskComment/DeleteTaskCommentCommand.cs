
using MediatR;


namespace Lunatic.Application.Features.Tasks.Commands.DeleteTaskComment {
    public class DeleteTaskCommentCommand : IRequest<DeleteTaskCommentCommandResponse> {
        public Guid TaskId { get; set; }
        public Guid CommentId { get; set; }
    }
}
