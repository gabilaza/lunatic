
using Lunatic.Domain.Entities;


namespace Lunatic.Infrastructure.Repositories {
    public class TeamRepository : AsyncRepository<Team> {
        public TeamRepository(LunaticContext context) : base(context) {
        }
    }
}

