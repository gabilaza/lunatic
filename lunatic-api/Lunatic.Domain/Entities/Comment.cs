
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Comment : AuditableEntity {
        private Comment(User addedBy, Task task, string content) {
            Id = Guid.NewGuid();
            AddedBy = addedBy;
            Task = task;
            Content = content;
            // emotes
        }

        public Guid Id { get; private set; }
        public User AddedBy { get; private set; }
        public Task Task { get; private set; }
        public string Content { get; private set; }

        public static Result<Comment> Create(User addedBy, Task task, string content) {
            if(addedBy == null) {
                return Result<Comment>.Failure("AddedByUser is required.");
            }

            if(task == null) {
                return Result<Comment>.Failure("Task is required.");
            }

            if(string.IsNullOrWhiteSpace(content)) {
                return Result<Comment>.Failure("Content is required.");
            }

            return Result<Comment>.Success(new Comment(addedBy, task, content));
        }
    }
}
