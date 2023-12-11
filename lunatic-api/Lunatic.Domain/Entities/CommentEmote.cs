
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class CommentEmote {
        private CommentEmote(Guid userId, Guid commentId, Emote emote) {
            CommentEmoteId = Guid.NewGuid();
            UserId = userId;
            CommentId = commentId;

            Emote = emote;
        }

        public Guid CommentEmoteId { get; private set; }
        public Guid UserId { get; private set; }
        public Guid CommentId { get; private set; }

        public Emote Emote { get; private set; }

        public static Result<CommentEmote> Create(Guid userId, Guid commentId, Emote emote) {
            if(userId == default) {
                return Result<CommentEmote>.Failure("User Id is required.");
            }

            if(commentId == default) {
                return Result<CommentEmote>.Failure("Comment Id is required.");
            }

            return Result<CommentEmote>.Success(new CommentEmote(userId, commentId, emote));
        }

        public void Update(Emote emote) {
            Emote = emote;
        }
    }
}

