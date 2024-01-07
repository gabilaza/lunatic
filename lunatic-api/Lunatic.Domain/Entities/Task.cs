
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
	public class Task : AuditableEntity {
		private Task(Guid createdByUserId, Guid projectId, string title, string description, TaskPriority priority) : base(createdByUserId) {
			TaskId = Guid.NewGuid();
			ProjectId = projectId;

			Title = title;
			Description = description;
			Priority = priority;
			Status = TaskStatus.CREATED;
		}

		public Guid TaskId { get; private set; }
		public Guid ProjectId { get; private set; }

		public string Title { get; private set; }
		public string Description { get; private set; }
		public TaskPriority Priority { get; private set; }
		public TaskStatus Status { get; private set; }
		//public int ProgressPercent { get; private set; }
		public List<string> Tags { get; private set; } = new();
		public List<Guid> CommentIds { get; private set; } = new();
		public List<Guid> AssigneeIds { get; private set; } = new();

		public DateTime? StartedDate { get; private set; }
		public DateTime? EndedDate { get; private set; }

		public static Result<Task> Create(Guid createdByUserId, Guid projectId, string title, string description, TaskPriority priority) {
			if (createdByUserId == default) {
				return Result<Task>.Failure("Created by User Id is required.");
			}

			if (projectId == default) {
				return Result<Task>.Failure("Project Id is required.");
			}

			if (string.IsNullOrWhiteSpace(title)) {
				return Result<Task>.Failure("Title is required.");
			}

			if (string.IsNullOrWhiteSpace(description)) {
				return Result<Task>.Failure("Description is required.");
			}

			return Result<Task>.Success(new Task(createdByUserId, projectId, title, description, priority));
		}

		public void Update(string title, string description, TaskPriority priority, TaskStatus status) {
			Title = title;
			Description = description;
			Priority = priority;
			Status = status;
		}

		public void AddTag(string tag) => Tags.Add(tag);
		public void RemoveTag(string tag) => Tags.Remove(tag);

		public void AddComment(Comment comment) => CommentIds.Add(comment.CommentId);
		public void AddComment(Guid commentId) => CommentIds.Add(commentId);
		public void RemoveComment(Comment comment) => CommentIds.Remove(comment.CommentId);
		public void RemoveComment(Guid commentId) => CommentIds.Remove(commentId);

		public void AddAssignee(User user) => AssigneeIds.Add(user.UserId);
		public void AddAssignee(Guid userId) => AssigneeIds.Add(userId);
		public void RemoveAssignee(User user) => AssigneeIds.Remove(user.UserId);
		public void RemoveAssignee(Guid userId) => AssigneeIds.Remove(userId);

		public void MarkAsInProgress() {
			if (!Status.IsCreated() && !Status.IsDone()) {
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
