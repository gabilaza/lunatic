
using Lunatic.Domain.Utils;

namespace Lunatic.Domain.Entities {
    public class CommentEmote {
        private CommentEmote(User user, Comment comment, Emote emote) {
            Id = Guid.NewGuid();
            User = user;
            Comment = comment;

            Emote = emote;
        }

        public Guid Id { get; private set; }
        public User User { get; private set; }
        public Comment Comment { get; private set; }

        public Emote Emote { get; private set; }

        public static Result<CommentEmote> Create(User user, Comment comment, Emote emote) {
            return Result<CommentEmote>.Success(new CommentEmote(user, comment, emote));
        }
    }
}

