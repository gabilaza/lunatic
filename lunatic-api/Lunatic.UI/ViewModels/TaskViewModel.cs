namespace Lunatic.UI.ViewModels {
	public class TaskViewModel {
		public Guid TaskId { get; set; }
		public Guid ProjectId { get; set; }

		public string Title { get; set; }
		public string Description { get; set; }
		public TaskPriority Priority { get; set; }
		public TaskStatus Status { get; set; }

		public List<string> Tags { get; set; } = null!;
		public List<Guid> CommentIds { get; set; } = null!;
		public List<Guid> AssigneeIds { get; set; } = null!;

		public DateTime? StartedDate { get; set; }
		public DateTime? EndedDate { get; set; }
	}
}
