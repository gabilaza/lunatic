using Lunatic.UI.Dtos;
using Lunatic.UI.Services.Responses;

namespace Lunatic.UI.Contracts {
	public interface ITaskDataService {	
		ApiResponse<TaskDto> GetTaskById(string taskId);
	}
}
