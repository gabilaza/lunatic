
using Lunatic.Domain.Models;
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class Task : AuditableEntity {
        private Task(Guid createdByUserId, Guid projectId, string title, string description, TaskPriority priority) : base(createdByUserId) {
            Id = Guid.NewGuid();
            ProjectId = projectId;
            Title = title;
            Description = description;
            Status = TaskStatus.CREATED;
            StartedDate = DateTime.UtcNow;
            Priority = priority;
        }

        public Guid Id { get; private set; }
        public Guid ProjectId { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public TaskPriority Priority { get; private set; }
        public TaskStatus Status { get; private set; }
        public List<Tag>? Tags { get; private set; }
        public List<Guid>? CommentIds { get; private set; }
        public List<Guid>? UserAssignIds { get; private set; }

        public DateTime? StartedDate { get; private set; }
        public DateTime? EndedDate { get; private set; }

        public static Result<Task> Create(Guid createdByUserId, Guid projectId, string title, string description, TaskPriority priority) {
            if (createdByUserId == default) {
                return Result<Task>.Failure("Created User id should not be default.");
            }
            if(projectId == default) {
                return Result<Task>.Failure("Project id should not be default.");
            }

            if (string.IsNullOrWhiteSpace(title)) {
                return Result<Task>.Failure("Title is required.");
            }

            if (string.IsNullOrWhiteSpace(description)) {
                return Result<Task>.Failure("Description is required.");
            }

            return Result<Task>.Success(new Task(createdByUserId, projectId, title, description, priority));
        }

        public void Update(string title, string description, TaskPriority priority, TaskStatus status, List<Tag>? tags, List<Guid>? commentIds, List<Guid>? userAssignIds, DateTime? startedDate, DateTime? endedDate) {
            Title = title;
            Description = description;
            Priority = priority;
            Status = status;
            Tags = tags;
            CommentIds = commentIds;
            UserAssignIds = userAssignIds;
            StartedDate = startedDate;
            EndedDate = endedDate;
        }

        public void AddTag(Tag tag) {
            if (Tags == null) {
                Tags = new List<Tag>();
            }

            Tags.Add(tag);
        }

        public void AddComment(Comment comment) {
            if(CommentIds == null) {
                CommentIds = new List<Guid>();
            }

            CommentIds.Add(comment.Id);
        }


        public void SetStatus(TaskStatus status) {
            switch (Status) {
                case TaskStatus.CREATED:
                    if (status == TaskStatus.IN_PROGRESS) {
                        Status = status;
                        StartedDate = DateTime.UtcNow;
                    } else {
                        // throw
                    }
                    break;
                case TaskStatus.IN_PROGRESS:
                    if (status == TaskStatus.DONE) {
                        Status = status;
                        EndedDate = DateTime.UtcNow;
                    } else {
                        // throw
                    }
                    break;
                case TaskStatus.DONE:
                    if (status == TaskStatus.IN_PROGRESS) {
                        Status = status;
                        EndedDate = DateTime.UtcNow;
                    } else {
                        // throw
                    }
                    break;
            }
        }

        public void AddUserAssign(User user) {
            if(UserAssignIds == null) {
                UserAssignIds = new List<Guid>();
            }

            UserAssignIds.Add(new Guid(user.Id));
        }
    }
}
