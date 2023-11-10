
using Lunatic.Domain.Entities;


namespace Lunatic.Infrastructure.Repositories {
    public class UserRepository : AsyncRepository<User> {
        public UserRepository(LunaticContext context) : base(context) {
        }
    }
}

