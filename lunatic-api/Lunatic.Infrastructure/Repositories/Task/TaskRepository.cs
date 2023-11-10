
using Lunatic.Domain.Entities;
using Task = Lunatic.Domain.Entities.Task;


namespace Lunatic.Infrastructure.Repositories {
    public class TaskRepository : AsyncRepository<Task> {
        public TaskRepository(LunaticContext context) : base(context) {
        }
    }
}

