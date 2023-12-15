using Lunatic.UI.Contracts;
using Lunatic.UI.Services.Responses;
using Lunatic.UI.ViewModels;
using System.Net.Http.Json;

namespace Lunatic.UI.Services {
	public class TeamDataService : ITeamDataService {
		private const string RequestUri = "api/v1/users";
		private readonly HttpClient httpClient;
		//private readonly ITokenService tokenService;

		public TeamDataService(HttpClient httpClient) {//, ITokenService tokenService
			this.httpClient = httpClient;
			//this.tokenService = tokenService;
		}

		public async Task<ApiResponse<TeamDto>> CreateTeamAsync(TeamViewModel teamViewModel) {
			//httpClient.DefaultRequestHeaders.Authorization
			//	= new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
			var result = await httpClient.PostAsJsonAsync("api/v1/teams", teamViewModel);
			result.EnsureSuccessStatusCode();
			var response = await result.Content.ReadFromJsonAsync<ApiResponse<TeamDto>>();
			response!.IsSuccess = result.IsSuccessStatusCode;
			return response!;
		}

		public Task<ApiResponse<TeamDto>> DeleteTeamAsync(Guid id) {
			throw new NotImplementedException();
		}


		public Task<ApiResponse<TeamDto>> UpdateTeamAsync(TeamViewModel team) {
			throw new NotImplementedException();
		}

		public async Task<List<TeamDto>> GetUserTeamsAsync(Guid userId) {
			var result = await httpClient.GetAsync($"{RequestUri}/{userId}/teams", HttpCompletionOption.ResponseHeadersRead);
			result.EnsureSuccessStatusCode();

			//var content = await result.Content.ReadAsStringAsync();
			//if (!result.IsSuccessStatusCode) {
			//	throw new ApplicationException(content);
			//}

			var response = await result.Content.ReadFromJsonAsync<List<TeamDto>>();
			//Debug.WriteLine($"response: {response}");
			//response!.IsSuccess = result.IsSuccessStatusCode;
			//return response.Data!;
			return response;
		}

		public async Task<TeamDto> GetTeamByIdAsync(string teamId) {
			var result = await httpClient.GetAsync($"api/v1/teams/{teamId}", HttpCompletionOption.ResponseHeadersRead);
			result.EnsureSuccessStatusCode();

			//var content = await result.Content.ReadAsStringAsync();
			//if (!result.IsSuccessStatusCode) {
			//	throw new ApplicationException(content);
			//}

			var response = await result.Content.ReadFromJsonAsync<TeamDto>();
			//Debug.WriteLine($"response: {response}");
			//response!.IsSuccess = result.IsSuccessStatusCode;
			//return response.Data!;
			return response!;

		}
	}
}
