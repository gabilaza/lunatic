
namespace Lunatic.Application.Features.Comments.Payload {
    public class CommentDto {
        public Guid Id { get; set; } = default!;
        public Guid TaskId { get; set; } = default!;

        public string Content { get; set; } = default!;

        public List<Guid> EmoteIds { get; set; } = default!;
    }
}
