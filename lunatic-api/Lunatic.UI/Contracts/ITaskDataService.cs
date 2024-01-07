using Lunatic.UI.Models.Dtos;
using Lunatic.UI.Services.Responses;

namespace Lunatic.UI.Contracts {
	public interface ITaskDataService {
		Task<ApiResponse<TaskDto>> GetTaskById(string taskId);
		//Task<ApiResponse> AddTaskSectionAsync(string taskId, string taskSection);
	}
}
