using Lunatic.UI.Contracts;
using Lunatic.UI.Services.Responses;
using Lunatic.UI.ViewModels;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Lunatic.UI.Services {
	public class TeamDataService : ITeamDataService {
		private const string RequestUri = "api/v1/teams";
		private readonly HttpClient httpClient;
		private readonly ITokenService tokenService;

		public TeamDataService(HttpClient httpClient, ITokenService tokenService) {//
			this.httpClient = httpClient;
			this.tokenService = tokenService;
		}

		public async Task<ApiResponse<TeamDto>> CreateTeamAsync(TeamViewModel teamViewModel) {
			httpClient.DefaultRequestHeaders.Authorization
				= new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
			var result = await httpClient.PostAsJsonAsync(RequestUri, teamViewModel);
			var response = await result.Content.ReadFromJsonAsync<ApiResponse<TeamDto>>();
			response!.Success = result.IsSuccessStatusCode;
			return response!;
		}

		public Task<ApiResponse<TeamDto>> DeleteTeamAsync(Guid id) {
			throw new NotImplementedException();
		}


		public Task<ApiResponse<TeamDto>> UpdateTeamAsync(TeamViewModel team) {
			throw new NotImplementedException();
		}

		public async Task<List<TeamDto>> GetUserTeamsAsync(Guid userId) {
			var result = await httpClient.GetAsync($"api/v1/users/{userId}/teams", HttpCompletionOption.ResponseHeadersRead);
			//result.EnsureSuccessStatusCode();
			var teams = await result.Content.ReadFromJsonAsync<ApiResponse<List<TeamDto>>>();
			return teams!.GetValue("teams");
		}

		public async Task<ApiResponse<TeamDto>> GetTeamByIdAsync(string teamId) {
			var result = await httpClient.GetAsync($"{RequestUri}/{teamId}", HttpCompletionOption.ResponseHeadersRead);
			var team = await result.Content.ReadFromJsonAsync<ApiResponse<TeamDto>>();
			return team!;
		}

		public async Task<bool> RemoveMemberFromTeamAsync(string memberId, string teamId) {
			var result = await httpClient.DeleteAsync($"{RequestUri}/{teamId}/members/{memberId}");
			return result.IsSuccessStatusCode;
		}

		public async Task<ApiResponse<TeamDto>> AddMemberToTeamAsync(string memberId, string teamId) {
			httpClient.DefaultRequestHeaders.Authorization
				= new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
			var result = await httpClient.PostAsJsonAsync($"{RequestUri}/{teamId}/members/",
				new AddTeamMemberViewModel() {
					UserId = memberId,
					TeamId = teamId
				});
			var response = await result.Content.ReadFromJsonAsync<ApiResponse<TeamDto>>();
			response!.Success = result.IsSuccessStatusCode;
			return response!;
		}

		public async Task<ApiResponse<ProjectDto>> AddProjectToTeamAsync(string teamId, ProjectViewModel projectViewModel) {
			httpClient.DefaultRequestHeaders.Authorization
				= new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
			var result = await httpClient.PostAsJsonAsync($"{RequestUri}/{teamId}/projects/", projectViewModel);
			var response = await result.Content.ReadFromJsonAsync<ApiResponse<ProjectDto>>();
			response!.Success = result.IsSuccessStatusCode;
			return response!;
		}
	}
}
