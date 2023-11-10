
using Lunatic.Domain.Entities;
using Task = Lunatic.Domain.Entities.Task;


namespace Lunatic.Infrastructure.Repositories {
    public interface ITaskRepository : Repository<Task> {
    }
}

