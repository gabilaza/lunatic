
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Comments.Payload;
using MediatR;


namespace Lunatic.Application.Features.Comments.Queries.GetById {
    public class GetByIdCommentQueryHandler : IRequestHandler<GetByIdCommentQuery, GetByIdCommentQueryResponse> {
        private readonly ICommentRepository commentRepository;

        public GetByIdCommentQueryHandler(ICommentRepository commentRepository) {
            this.commentRepository = commentRepository;
        }

        public async Task<GetByIdCommentQueryResponse> Handle(GetByIdCommentQuery request, CancellationToken cancellationToken) {
            var commentResult = await this.commentRepository.FindByIdAsync(request.Id);
            if(!commentResult.IsSuccess) {
                return new GetByIdCommentQueryResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Comment not found" }
                };
            }

            return new GetByIdCommentQueryResponse {
                Success = true,
                Comment = new CommentDto {
                    Id = commentResult.Value.Id,
                    TaskId = commentResult.Value.TaskId,

                    Content = commentResult.Value.Content,

                    EmoteIds = commentResult.Value.EmoteIds,
                }
            };
        }
    }
}
