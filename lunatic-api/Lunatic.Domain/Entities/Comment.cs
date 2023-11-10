
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Comment : AuditableEntity {
        private Comment(Guid userId, Guid taskId, string content) {
            Id = Guid.NewGuid();
            UserId = userId;
            TaskId = taskId;
            Content = content;
            // emotes
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Guid TaskId { get; private set; }
        public string Content { get; private set; }

        public static Result<Comment> Create(Guid userId, Guid taskId, string content) {
            if(userId == default) {
                return Result<Comment>.Failure("User id should not be default.");
            }

            if(taskId == default) {
                return Result<Comment>.Failure("Task id should not be default.");
            }

            if(string.IsNullOrWhiteSpace(content)) {
                return Result<Comment>.Failure("Content is required.");
            }

            return Result<Comment>.Success(new Comment(userId, taskId, content));
        }
    }
}
