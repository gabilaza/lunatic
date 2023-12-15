using System.ComponentModel.DataAnnotations;

namespace Lunatic.UI.ViewModels {
	public class TeamViewModel {
		public Guid OwnerId { get; set; }

		[Required(ErrorMessage = "Event name is required")]
		[StringLength(50, ErrorMessage = "Event name is too long")]
		public string Name { get; set; } = null!;

		public string? Description { get; set; }

		[Required]
		public List<Guid> MemberIds { get; set; } = null!;

		public List<Guid>? ProjectIds { get; set; }
	}
}