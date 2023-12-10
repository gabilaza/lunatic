
using Lunatic.Domain.Entities;
using MediatR;


namespace Lunatic.Application.Features.Comments.Commands.UpdateComment {
    public class UpdateCommentCommand : IRequest<UpdateCommentCommandResponse> {
        public Guid Id { get; set; } = default!;

        public string Content { get; set; } = default!;
    }
}
