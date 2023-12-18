using Lunatic.UI.Services.Responses;
using Lunatic.UI.ViewModels;

namespace Lunatic.UI.Contracts {
	public interface IUserDataService {
		Task<ApiResponse<UserDto>> GetUserByIdAsync(string id);
	}
}
