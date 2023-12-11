
using Lunatic.Domain.Entities;


namespace Lunatic.Application.Features.CommentEmotes.Payload {
    public class CommentEmoteDto {
        public Guid Id { get; set; } = default!;
        public Guid UserId { get; set; } = default!;
        public Guid CommentId { get; set; } = default!;

        public Emote Emote { get; set; } = default!;
    }
}
