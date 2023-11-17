
using Lunatic.Application.Persistence;
using Task = Lunatic.Domain.Entities.Task;


namespace Lunatic.Infrastructure.Repositories {
    public class TaskRepository : RepositoryBase<Task>, ITaskRepository {
        public TaskRepository(LunaticContext context) : base(context) {
        }
    }
}

