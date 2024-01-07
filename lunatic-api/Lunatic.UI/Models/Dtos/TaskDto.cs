using Lunatic.UI.Models.Shared;

namespace Lunatic.UI.Models.Dtos {
	public class TaskDto {
		public string TaskId { get; set; } = null!;
		public string ProjectId { get; set; } = null!;
		public string Title { get; set; } = null!;
		public string Description { get; set; } = null!;
		public TaskPriority Priority { get; set; }
		public Shared.TaskStatus Status { get; set; }
		public List<string> Tags { get; set; } = null!;
		public List<string> CommentIds { get; set; } = null!;
		public List<string> AssigneeIds { get; set; } = null!;
		public DateTime PlannedStartDate { get; set; }
		public DateTime PlannedEndDate { get; set; }
		public DateTime? StartedDate { get; set; }
		public DateTime? EndedDate { get; set; }
	}
}