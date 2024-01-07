using Lunatic.UI.Models.Shared;

namespace Lunatic.UI.Models.ViewModels {
	public class TaskViewModel {
		public string ProjectId { get; set; } = null!;
		public string Title { get; set; } = null!;
		public string Description { get; set; } = null!;
		public TaskPriority Priority { get; set; }
		public Shared.TaskStatus Status { get; set; }
		public List<string> Tags { get; set; } = null!;
		public List<string> AssigneeIds { get; set; } = null!;
		public DateTime PlannedStartDate { get; set; } //for UI 
		public DateTime PlannedEndDate { get; set; }
	}
}
