
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Task : AuditableEntity {
        private Task(Guid createdByUserId, Project project, string title, string description, TaskPriority priority) : base(createdByUserId) {
            Id = Guid.NewGuid();
            Project = project;

            Title = title;
            Description = description;
            Priority = priority;
            Status = TaskStatus.CREATED;
        }

        public Guid Id { get; private set; }
        public Project Project { get; private set; }

        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskPriority Priority { get; private set; }
        public TaskStatus  Status { get; private set; }

        public ICollection<Tag> Tags { get; private set; } = new List<Tag>();
        public ICollection<Guid> CommentIds { get; private set; } = new List<Guid>();
        public ICollection<Guid> AssigneeIds { get; private set; } = new List<Guid>();

        public DateTime? StartedDate { get; private set; }
        public DateTime? EndedDate { get; private set; }

        public static Result<Task> Create(Guid createdByUserId, Project project, string title, string description, TaskPriority priority) {
            if(createdByUserId == default) {
                return Result<Task>.Failure("Created by User Id is required.");
            }

            if(string.IsNullOrWhiteSpace(title)) {
                return Result<Task>.Failure("Title is required.");
            }

            if(string.IsNullOrWhiteSpace(description)) {
                return Result<Task>.Failure("Description is required.");
            }

            return Result<Task>.Success(new Task(createdByUserId, project, title, description, priority));
        }

        public void Update(string title, string description, TaskPriority priority, TaskStatus status) {
            Title = title;
            Description = description;
            Priority = priority;
            Status = status;
        }

        public void AddTag(Tag tag) => Tags.Add(tag);
        public void RemoveTag(Tag tag) => Tags.Remove(tag);

        public void AddComment(Comment comment) => CommentIds.Add(comment.Id);
        public void RemoveComment(Comment comment) => CommentIds.Remove(comment.Id);

        public void AddAssignee(User user) => AssigneeIds.Add(user.Id);
        public void RemoveAssignee(User user) => AssigneeIds.Remove(user.Id);

        public void MarkAsInProgress() {
            if(!Status.IsCreated() && !Status.IsDone()) {
                throw new InvalidActionException($"Can't mark as in progress if you are in status {Status}");
            }
            Status = TaskStatus.IN_PROGRESS;
            StartedDate = DateTime.UtcNow;
            EndedDate = null;
        }

        public void MarkAsDone() {
            Status = TaskStatus.DONE;
            EndedDate = DateTime.UtcNow;
        }
    }
}
