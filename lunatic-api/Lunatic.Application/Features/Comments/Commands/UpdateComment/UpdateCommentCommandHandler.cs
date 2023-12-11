
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Comments.Payload;
using MediatR;


namespace Lunatic.Application.Features.Comments.Commands.UpdateComment {
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, UpdateCommentCommandResponse> {
        private readonly ICommentRepository commentRepository;

        public UpdateCommentCommandHandler(ICommentRepository commentRepository) {
            this.commentRepository = commentRepository;
        }

        public async Task<UpdateCommentCommandResponse> Handle(UpdateCommentCommand request, CancellationToken cancellationToken) {
            var validator = new UpdateCommentCommandValidator();
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new UpdateCommentCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var commentResult = await this.commentRepository.FindByIdAsync(request.CommentId);
            if(!commentResult.IsSuccess) {
                return new UpdateCommentCommandResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Comment not found" }
                };
            }

            commentResult.Value.Update(request.Content);

            var dbCommentResult = await this.commentRepository.UpdateAsync(commentResult.Value);

            return new UpdateCommentCommandResponse {
                Success = true,
                Comment = new CommentDto {
                    CommentId = dbCommentResult.Value.CommentId,
                    TaskId = dbCommentResult.Value.TaskId,

                    Content = dbCommentResult.Value.Content,

                    EmoteIds = dbCommentResult.Value.EmoteIds,
                }
            };
        }
    }
}
