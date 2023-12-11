
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Comments.Payload;
using MediatR;


namespace Lunatic.Application.Features.Comments.Queries.GetAll {
    public class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, GetAllCommentsQueryResponse> {
        private readonly ICommentRepository commentRepository;

        public GetAllCommentsQueryHandler(ICommentRepository commentRepository) {
            this.commentRepository = commentRepository;
        }

        public async Task<GetAllCommentsQueryResponse> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken) {
            GetAllCommentsQueryResponse response = new GetAllCommentsQueryResponse();
            var comments = await this.commentRepository.GetAllAsync();

            if(comments.IsSuccess) {
                response.Comments = comments.Value.Select(comment => new CommentDto {
                    CommentId = comment.CommentId,
                    TaskId = comment.TaskId,

                    Content = comment.Content,

                    EmoteIds = comment.EmoteIds,
                }).ToList();
            }
            return response;
        }
    }
}
