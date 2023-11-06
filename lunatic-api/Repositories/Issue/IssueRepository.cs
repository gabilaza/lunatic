
using Entities;

namespace Repositories {
    public class IssueRepository : AsyncRepository<Issue> {
        public IssueRepository(LunaticContext context) : base(context) {
        }
    }
}

