using Lunatic.UI.Services.Responses;

namespace Lunatic.UI.Contracts {
    public interface ITeamDataService {
        Task<List<TeamViewModel>> GetUserTeamsAsync(Guid id);
        Task<ApiResponse<Guid>> CreateTeamAsync(TeamViewModel team);
        Task<ApiResponse<Guid>> UpdateTeamAsync(TeamViewModel team);
        Task<ApiResponse<Guid>> DeleteTeamAsync(Guid id);
    }
}
