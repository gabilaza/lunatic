using Lunatic.UI.Contracts;
using Lunatic.UI.Services.Responses;
using Lunatic.UI.ViewModels;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Lunatic.UI.Services {
	public class UserDataService : IUserDataService {
		private const string RequestUri = "api/v1/users";
		private readonly HttpClient httpClient;
		private readonly ITokenService tokenService;

		public UserDataService(HttpClient httpClient, ITokenService tokenService) {
			this.httpClient = httpClient;
			this.tokenService = tokenService;
		}

		public async Task<ApiResponse<UserDto>> GetUserByIdAsync(string id) {
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await tokenService.GetTokenAsync());

			var response = await JsonSerializer.DeserializeAsync<ApiResponse<UserDto>>(
				await httpClient.GetStreamAsync($"{RequestUri}/{id}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
				);

			return response!;
		}

		//only username and userid are brought!
		public async Task<List<UserDto>> GetUsersByUsernameAsync(string usernameMatch) {
			httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await tokenService.GetTokenAsync());
			var response = await JsonSerializer.DeserializeAsync<List<UserDto>>(
				await httpClient.GetStreamAsync($"{RequestUri}/usernames/{usernameMatch}"),
				new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
				);
			return response!;
		}

		//public async Task<ApiResponse<bool>> RemoveUserFromTeamAsync(string memberId, string teamId) {
		//	httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await tokenService.GetTokenAsync());

		//	var response = await JsonSerializer.DeserializeAsync<List<UserDto>>(
		//		await httpClient.GetStreamAsync($"{RequestUri}/usernames/{usernameMatch}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
		//		);

		//}

		//public async Task UpdateUserAsync(UserDto user) {
		//	httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await tokenService.GetTokenAsync());
		//	var userJson = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
		//	await httpClient.PutAsync("api/users", userJson);
		//}
	}
}
