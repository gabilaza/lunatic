
using Lunatic.Domain.Entities;


namespace Lunatic.Infrastructure.Repositories {
    public class TaskRepository : AsyncRepository<Task> {
        public TaskRepository(LunaticContext context) : base(context) {
        }
    }
}

