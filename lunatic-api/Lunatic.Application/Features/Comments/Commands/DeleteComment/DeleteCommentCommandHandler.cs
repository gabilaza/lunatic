
using Lunatic.Application.Persistence;
using MediatR;


namespace Lunatic.Application.Features.Comments.Commands.DeleteComment {
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, DeleteCommentCommandResponse> {
        private readonly ICommentRepository commentRepository;

        public DeleteCommentCommandHandler(ICommentRepository commentRepository) {
            this.commentRepository = commentRepository;
        }

        public async Task<DeleteCommentCommandResponse> Handle(DeleteCommentCommand request, CancellationToken cancellationToken) {
            var result = await this.commentRepository.DeleteAsync(request.Id);

            if(!result.IsSuccess) {
                return new DeleteCommentCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { result.Error }
                };

            }
            return new DeleteCommentCommandResponse {
                Success = true
            };
        }
    }
}
