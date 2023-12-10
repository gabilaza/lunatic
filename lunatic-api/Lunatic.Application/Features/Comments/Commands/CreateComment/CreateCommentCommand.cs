
using Lunatic.Domain.Entities;
using MediatR;


namespace Lunatic.Application.Features.Comments.Commands.CreateComment {
    public class CreateCommentComand : IRequest<CreateCommentCommandResponse> {
        public Guid UserId { get; set; } = default!;
        public Guid TaskId { get; set; } = default!;

        public string Content { get; set; } = default!;
    }
}
