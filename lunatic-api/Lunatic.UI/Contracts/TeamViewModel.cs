using System.ComponentModel.DataAnnotations;

namespace Lunatic.UI.Contracts {
    public class TeamViewModel {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Event name is required")]
        [StringLength(50, ErrorMessage = "Event name is too long")]
        public string Name { get; set; } = null!;
        [Required]
        public List<Guid> MemberIds { get; set; } = null!;
        [Required]
        public List<Guid> ProjectIds { get; set; } = null!;
    }
}