
using Entities;

namespace Repositories {
    public class UserRepository : AsyncRepository<User> {
        public UserRepository(LunaticContext context) : base(context) {
        }
    }
}

