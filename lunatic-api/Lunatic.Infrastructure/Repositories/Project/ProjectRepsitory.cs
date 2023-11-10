
using Lunatic.Domain.Entities;


namespace Lunatic.Infrastructure.Repositories {
    public class ProjectRepository : AsyncRepository<Project> {
        public ProjectRepository(LunaticContext context) : base(context) {
        }
    }
}

