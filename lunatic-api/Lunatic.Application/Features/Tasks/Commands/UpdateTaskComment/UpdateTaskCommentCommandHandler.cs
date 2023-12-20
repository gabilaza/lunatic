
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Comments.Payload;
using MediatR;


namespace Lunatic.Application.Features.Tasks.Commands.UpdateTaskComment {
    public class UpdateTaskCommentCommandHandler : IRequestHandler<UpdateTaskCommentCommand, UpdateTaskCommentCommandResponse> {
        private readonly ICommentRepository commentRepository;

        public UpdateTaskCommentCommandHandler(ICommentRepository commentRepository) {
            this.commentRepository = commentRepository;
        }

        public async Task<UpdateTaskCommentCommandResponse> Handle(UpdateTaskCommentCommand request, CancellationToken cancellationToken) {
            var validator = new UpdateTaskCommentCommandValidator(this.commentRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new UpdateTaskCommentCommandResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var commentResult = await this.commentRepository.FindByIdAsync(request.CommentId);

            commentResult.Value.Update(request.Content);

            var dbCommentResult = await this.commentRepository.UpdateAsync(commentResult.Value);

            return new UpdateTaskCommentCommandResponse {
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
