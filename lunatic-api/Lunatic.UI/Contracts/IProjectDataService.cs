using Lunatic.UI.Services;
using Lunatic.UI.Services.Responses;
using Lunatic.UI.ViewModels;

namespace Lunatic.UI.Contracts {
	public interface IProjectDataService {
		Task<ApiResponse<ProjectDto>> CreateProjectAsync(ProjectViewModel Project);
		Task<ApiResponse<ProjectDto>> UpdateProjectAsync(ProjectViewModel Project);
		Task<ApiResponse<ProjectDto>> DeleteProjectAsync(Guid id);
		Task<ProjectDto> GetProjectByIdAsync(string teamId);
	}
}

