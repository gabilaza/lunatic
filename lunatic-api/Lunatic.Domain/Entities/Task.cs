
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Task : AuditableEntity {
        private Task(User createdByUser, Project project, string title, string description, TaskPriority priority) : base(createdByUser) {
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
        public ICollection<Comment> Comments { get; private set; } = new List<Comment>();
        public ICollection<User> Assignees { get; private set; } = new List<User>();

        public DateTime? StartedDate { get; private set; }
        public DateTime? EndedDate { get; private set; }

        public static Result<Task> Create(User createdByUser, Project project, string title, string description, TaskPriority priority) {
            if(string.IsNullOrWhiteSpace(title)) {
                return Result<Task>.Failure("Title is required.");
            }

            if(string.IsNullOrWhiteSpace(description)) {
                return Result<Task>.Failure("Description is required.");
            }

            return Result<Task>.Success(new Task(createdByUser, project, title, description, priority));
        }

        public void Update(string title, string description, TaskPriority priority, TaskStatus status, List<Tag> tags, List<Comment> comments, List<User> assignees, DateTime startedDate, DateTime endedDate) {
            Title = title;
            Description = description;
            Priority = priority;
            Status = status;
            Tags = tags;
            Comments = comments;
            Assignees = assignees;
            StartedDate = startedDate;
            EndedDate = endedDate;
        }

        public void AddTag(Tag tag) => Tags.Add(tag);
        public void RemoveTag(Tag tag) => Tags.Remove(tag);

        public void AddComment(Comment comment) => Comments.Add(comment);
        public void RemoveComment(Comment comment) => Comments.Remove(comment);

        public void AddAssignee(User user) => Assignees.Add(user);
        public void RemoveAssignee(User user) => Assignees.Remove(user);

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
