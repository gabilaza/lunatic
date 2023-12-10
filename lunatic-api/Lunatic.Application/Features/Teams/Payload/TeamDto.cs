
namespace Lunatic.Application.Features.Teams.Payload {
    public class TeamDto {
        public Guid Id { get; set; } = default!;

        public string Name { get; set; } = default!;

        public List<Guid> MemberIds { get; set; } = default!;
        public List<Guid> ProjectIds { get; set; } = default!;
    }
}
