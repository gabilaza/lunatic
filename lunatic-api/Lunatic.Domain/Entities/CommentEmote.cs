
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class CommentEmote {
        private CommentEmote(Guid userId, Emote emote) {
            Id = Guid.NewGuid();
            UserId = userId;
            // hm.. commentid
            Emote = emote;
        }

        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public Emote Emote { get; private set; }

        public static Result<CommentEmote> Create(Guid userId, Emote emote) {
            if(userId == default) {
                return Result<CommentEmote>.Failure("User id should not be default.");
            }

            return Result<CommentEmote>.Success(new CommentEmote(userId, emote));
        }
    }
}

