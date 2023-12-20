
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Comments.Payload;
using MediatR;


namespace Lunatic.Application.Features.Tasks.Queries.GetByIdComment {
    public class GetByIdTaskCommentQueryHandler : IRequestHandler<GetByIdTaskCommentQuery, GetByIdTaskCommentQueryResponse> {
        private readonly ICommentRepository commentRepository;

        public GetByIdTaskCommentQueryHandler(ICommentRepository commentRepository) {
            this.commentRepository = commentRepository;
        }

        public async Task<GetByIdTaskCommentQueryResponse> Handle(GetByIdTaskCommentQuery request, CancellationToken cancellationToken) {
            var validator = new GetByIdTaskCommentQueryValidator(this.commentRepository);
            var validatorResult = await validator.ValidateAsync(request, cancellationToken);

            if(!validatorResult.IsValid) {
                return new GetByIdTaskCommentQueryResponse {
                    Success = false,
                    ValidationErrors = validatorResult.Errors.Select(e => e.ErrorMessage).ToList()
                };
            }

            var comment = (await this.commentRepository.FindByIdAsync(request.CommentId)).Value;

            return new GetByIdTaskCommentQueryResponse {
                Success = true,
                Comment = new CommentDto {
                    CommentId = comment.CommentId,
                    TaskId = comment.TaskId,

                    Content = comment.Content,

                    EmoteIds = comment.EmoteIds,
                }
            };
        }
    }
}
