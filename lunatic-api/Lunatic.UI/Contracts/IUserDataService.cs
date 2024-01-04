using Lunatic.UI.Dtos;
using Lunatic.UI.Services.Responses;
using Lunatic.UI.ViewModels;

namespace Lunatic.UI.Contracts
{
    public interface IUserDataService {
		Task<ApiResponse<UserDto>> GetUserByIdAsync(string id);
		Task<List<UserDto>> GetUsersByUsernameAsync(string usernameMatch);
		Task<ApiResponse> UpdateUserInfoAsync(UserViewModel user);
		
	}
}
