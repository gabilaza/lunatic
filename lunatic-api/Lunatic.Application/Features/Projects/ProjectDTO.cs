
namespace Lunatic.Application.Features.Projects {
    public class ProjectDTO {
        public Guid Id { get; set; }
        public Guid CreatedByUserId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public List<Guid>? TaskIds { get; set; }
    }
}
