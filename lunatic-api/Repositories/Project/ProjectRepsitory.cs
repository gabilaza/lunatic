
using Entities;

namespace Repositories {
    public class ProjectRepository : AsyncRepository<Project> {
        public ProjectRepository(LunaticContext context) : base(context) {
        }
    }
}

