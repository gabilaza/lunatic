using Lunatic.UI.Models.Dtos;
using Lunatic.UI.Models.ViewModels;
using Lunatic.UI.Services.Responses;

namespace Lunatic.UI.Contracts
{
    public interface IUserDataService {
		Task<ApiResponse<UserDto>> GetUserByIdAsync(string id);
		Task<List<UserDto>> GetUsersByUsernameAsync(string usernameMatch);
		Task<ApiResponse> UpdateUserInfoAsync(UserViewModel user);
		
	}
}
