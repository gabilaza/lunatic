using Lunatic.UI.Contracts;
using Lunatic.UI.Services.Responses;
using Lunatic.UI.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

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
			httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("bearer", await tokenService.GetTokenAsync());
			var result = await httpClient.GetAsync($"{RequestUri}/{id}", HttpCompletionOption.ResponseHeadersRead);
			var response = await result.Content.ReadFromJsonAsync<ApiResponse<UserDto>>();
			response!.Success = result.IsSuccessStatusCode;
			return response!;
		}

		//only username and userid are brought!
		public async Task<List<UserDto>> GetUsersByUsernameAsync(string usernameMatch) {
			httpClient.DefaultRequestHeaders.Authorization =
				new AuthenticationHeaderValue("bearer", await tokenService.GetTokenAsync());
			var result = await httpClient.GetAsync($"{RequestUri}/usernames/{usernameMatch}", HttpCompletionOption.ResponseHeadersRead);
			var response = await result.Content.ReadFromJsonAsync<List<UserDto>>();
			return response!;
		}

	}
}
