
using Lunatic.Domain.Entities;
using Lunatic.Application.Persistence;
using Lunatic.Application.Features.Comments.Payload;
using MediatR;


namespace Lunatic.Application.Features.Tasks.Queries.GetAllComments {
    public class GetAllTaskCommentsQueryHandler : IRequestHandler<GetAllTaskCommentsQuery, GetAllTaskCommentsQueryResponse> {
        private readonly ITaskRepository taskRepository;

        private readonly ICommentRepository commentRepository;

        public GetAllTaskCommentsQueryHandler(ITaskRepository taskRepository, ICommentRepository commentRepository) {
            this.taskRepository = taskRepository;
            this.commentRepository = commentRepository;
        }

        public async Task<GetAllTaskCommentsQueryResponse> Handle(GetAllTaskCommentsQuery request, CancellationToken cancellationToken) {
            var taskResult = await this.taskRepository.FindByIdAsync(request.TaskId);
            if(!taskResult.IsSuccess) {
                return new GetAllTaskCommentsQueryResponse {
                    Success = false,
                    ValidationErrors = new List<string> { "Task not found" }
                };
            }

            GetAllTaskCommentsQueryResponse response = new GetAllTaskCommentsQueryResponse();
            var commentIds = taskResult.Value.CommentIds;
            var comments = new List<Comment>();
            foreach (var commentId in commentIds) {
                var comment = (await this.commentRepository.FindByIdAsync(commentId)).Value;
                comments.Add(comment);
            }

            response.Comments = comments.Select(comment => new CommentDto {
                CommentId = comment.CommentId,
                TaskId = comment.TaskId,

                Content = comment.Content,

                EmoteIds = comment.EmoteIds,
            }).ToList();
            return response;
        }
    }
}
