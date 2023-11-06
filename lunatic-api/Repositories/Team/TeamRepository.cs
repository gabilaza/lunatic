
using Entities;

namespace Repositories {
    public class TeamRepository : AsyncRepository<Team> {
        public TeamRepository(LunaticContext context) : base(context) {
        }
    }
}

