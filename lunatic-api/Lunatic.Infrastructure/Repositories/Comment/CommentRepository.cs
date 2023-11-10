
using Lunatic.Domain.Entities;


namespace Lunatic.Infrastructure.Repositories {
    public class CommentRepository : AsyncRepository<Comment> {
        public CommentRepository(LunaticContext context) : base(context) {
        }
    }
}

