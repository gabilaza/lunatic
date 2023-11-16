
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Comment : AuditableEntity {
        private Comment(Guid createdByUserId, Guid taskId, string content) : base(createdByUserId) {
            Id = Guid.NewGuid();
            TaskId = taskId;
            Content = content;
        }

        public Guid Id { get; private set; }
        public Guid TaskId { get; private set; }
        public string Content { get; private set; }
        public List<CommentEmote>? Emotes { get; private set; }

        public static Result<Comment> Create(Guid createdByUserId, Guid taskId, string content) {
            if(createdByUserId == default) {
                return Result<Comment>.Failure("Created User id should not be default.");
            }

            if(taskId == default) {
                return Result<Comment>.Failure("Task id should not be default.");
            }

            if(string.IsNullOrWhiteSpace(content)) {
                return Result<Comment>.Failure("Content is required.");
            }

            return Result<Comment>.Success(new Comment(createdByUserId, taskId, content));
        }
    }
}
