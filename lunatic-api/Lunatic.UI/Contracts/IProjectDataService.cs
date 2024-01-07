using Lunatic.UI.Models.Dtos;
using Lunatic.UI.Models.ViewModels;
using Lunatic.UI.Services.Responses;

namespace Lunatic.UI.Contracts
{
    public interface IProjectDataService {
		Task<ApiResponse<ProjectDto>> CreateProjectAsync(ProjectViewModel Project);
		Task<ApiResponse<ProjectDto>> UpdateProjectAsync(ProjectViewModel Project);
		Task<ApiResponse<ProjectDto>> DeleteProjectAsync(Guid id);
		Task<ApiResponse<ProjectDto>> GetProjectByIdAsync(string teamId);
		Task<ApiResponse<List<TaskDto>>> GetProjectTasksAsync(Guid projectId);

		Task<ApiResponse> AddSectionAsync(string projectId, string sectionTitle);
	}
}

