
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Task : AuditableEntity {
        private Task(Guid userId, string title, string description, TaskPriority priority) {
            Id = Guid.NewGuid();
            Title = title;
            Description = description;
            UserId = userId;
            Status = TaskStatus.CREATED;
            Priority = priority;
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskPriority Priority { get; private set; }
        public TaskStatus  Status { get; private set; }
        public List<Tag>? Tags { get; private set; }
        public List<Comment>? Comments { get; private set; }
        public List<Guid>? Assignees { get; private set; }

        public DateTime? StartedDate { get; private set; }
        public DateTime? EndedDate { get; private set; }

        public static Result<Task> Create(Guid userId, string title, string description, TaskPriority priority) {
            if(userId == default) {
                return Result<Task>.Failure("User id should not be default.");
            }

            if(string.IsNullOrWhiteSpace(title)) {
                return Result<Task>.Failure("Title is required.");
            }

            if(string.IsNullOrWhiteSpace(description)) {
                return Result<Task>.Failure("Description is required.");
            }

            return Result<Task>.Success(new Task(userId, title, description, priority));
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
            switch(Status) {
                case TaskStatus.CREATED:
                    if(status == TaskStatus.IN_PROGRESS) {
                        Status = status;
                        StartedDate = DateTime.Now;
                    } else {
                        // throw
                    }
                    break;
                case TaskStatus.IN_PROGRESS:
                    if(status == TaskStatus.DONE) {
                        Status = status;
                        EndedDate = DateTime.Now;
                    } else {
                        // throw
                    }
                    break;
                case TaskStatus.DONE:
                    if(status == TaskStatus.IN_PROGRESS) {
                        Status = status;
                        EndedDate = DateTime.Now;
                    } else {
                        // throw
                    }
                    break;
            }
        }

        public void AddAssignee(User user) {
            if(Assignees == null) {
                Assignees = new List<Guid>();
            }

            Assignees.Add(user.Id);
        }
    }
}
