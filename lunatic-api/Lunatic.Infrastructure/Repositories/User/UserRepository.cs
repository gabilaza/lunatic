
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;


namespace Lunatic.Infrastructure.Repositories {
    public class UserRepository : RepositoryBase<User>, IUserRepository {
        public UserRepository(LunaticContext context) : base(context) {
        }
    }
}

