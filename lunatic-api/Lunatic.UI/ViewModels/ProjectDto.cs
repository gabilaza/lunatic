﻿namespace Lunatic.UI.ViewModels {
	public class ProjectDto {
		public Guid ProjectId { get; set; } = default!;
		public Guid TeamId { get; set; } = default!;

		public string Title { get; set; } = default!;
		public string Description { get; set; } = default!;

		public List<Guid> TaskIds { get; set; } = default!;
	}
}
