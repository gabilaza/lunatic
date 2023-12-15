namespace Lunatic.UI.ViewModels {
	public class TeamDto {
		public Guid Id { get; set; }
		public string Name { get; set; } = string.Empty;
		public string Description { get; set; } = string.Empty;
		public List<Guid> MemberIds { get; set; } = []; //TODO! check if initialized already
		public List<Guid> ProjectIds { get; set; } = [];
	}
}
