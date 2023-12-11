
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;


namespace Lunatic.Infrastructure.Repositories {
    public class CommentEmoteRepository : RepositoryBase<CommentEmote>, ICommentEmoteRepository {
        public CommentEmoteRepository(LunaticContext context) : base(context) {
        }
    }
}

