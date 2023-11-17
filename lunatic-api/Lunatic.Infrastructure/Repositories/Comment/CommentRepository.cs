
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;


namespace Lunatic.Infrastructure.Repositories {
    public class CommentRepository : RepositoryBase<Comment>, ICommentRepository {
        public CommentRepository(LunaticContext context) : base(context) {
        }
    }
}

