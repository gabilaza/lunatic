
using MediatR;


namespace Lunatic.Application.Features.Tasks.Commands.CreateTaskComment {
    public class CreateTaskCommentCommand : IRequest<CreateTaskCommentCommandResponse> {
        public Guid UserId { get; set; } = default!;
        public Guid TaskId { get; set; } = default!;

        public string Content { get; set; } = default!;
    }
}
