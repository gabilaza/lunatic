using Lunatic.UI.Dtos;
using Lunatic.UI.Services.Responses;

namespace Lunatic.UI.Contracts
{
    public interface ITaskService {
        ApiResponse<List<TaskDto>> GetTasks();
    }
}
