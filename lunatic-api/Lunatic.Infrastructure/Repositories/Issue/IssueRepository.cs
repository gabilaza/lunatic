
using Lunatic.Domain.Entities;


namespace Lunatic.Infrastructure.Repositories {
    public class IssueRepository : AsyncRepository<Issue> {
        public IssueRepository(LunaticContext context) : base(context) {
        }
    }
}

