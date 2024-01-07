using Lunatic.UI.Models.Dtos;
using Lunatic.UI.Models.ViewModels;
using Lunatic.UI.Services.Responses;

namespace Lunatic.UI.Contracts
{
    public interface ITeamDataService {
		Task<ApiResponse<TeamDto>> CreateTeamAsync(TeamViewModel team);
		//Task<ApiResponse<TeamDto>> UpdateTeamAsync(TeamViewModel team);
		Task<ApiResponse<TeamDto>> UpdateTeamInfoAsync(UpdateTeamInfoViewModel viewModel);
		Task<ApiResponse> DeleteTeamAsync(string teamId);
		Task<ApiResponse<List<TeamDto>>> GetUserTeamsAsync(Guid userId);
		Task<ApiResponse<TeamDto>> GetTeamByIdAsync(string teamId);
		Task<bool> RemoveMemberFromTeamAsync(string memberId, string teamId);
		Task<ApiResponse<TeamDto>> AddMemberToTeamAsync(string memberId, string teamId);
		Task<ApiResponse<ProjectDto>> AddProjectToTeamAsync(string teamId, ProjectViewModel projectViewModel);

		Task<ApiResponse<List<UserDto>>> GetTeamMembersAsync(Guid teamId);

	}
}
