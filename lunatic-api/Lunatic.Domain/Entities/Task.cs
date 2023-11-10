
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Task : AuditableEntity {
        private Task(Guid userId, string title, string description) {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            UserId = userId;
            Status = TaskStatus.CREATED;
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskStatus  Status { get; private set; }
        public List<Tag>? Tags { get; private set; }
        public List<Comment>? Comments { get; private set; }

        public static Result<Task> Create(Guid userId, string title, string description) {
            if(userId == default) {
                return Result<Task>.Failure("User id should not be default.");
            }

            if(string.IsNullOrWhiteSpace(title)) {
                return Result<Task>.Failure("Title is required.");
            }

            if(string.IsNullOrWhiteSpace(description)) {
                return Result<Task>.Failure("Description is required.");
            }

            return Result<Task>.Success(new Task(userId, title, description));
        }

        public void AddTag(Tag tag) {
            if(Tags == null) {
                Tags = new List<Tag>();
            }

            Tags.Add(tag);
        }

        public void AddComment(Comment comment) {
            if(Comments == null) {
                Comments = new List<Comment>();
            }

            Comments.Add(comment);
        }

        // hm..
        public void SetStatus(TaskStatus status) {
            Status = status;
        }
    }
}
