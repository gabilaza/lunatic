using Lunatic.UI.ViewModels;

namespace Lunatic.UI.Contracts {
	public interface IProjectDataService {

		Task<ProjectDto> GetProjectByIdAsync(string teamId);
	}
}
