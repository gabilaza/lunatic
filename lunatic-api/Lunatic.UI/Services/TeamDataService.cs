﻿using Lunatic.UI.Contracts;
using Lunatic.UI.Services.Responses;
using Lunatic.UI.ViewModels;
using System.Net.Http.Json;
using System.Net.Http.Headers;
using System.Text.Json;

namespace Lunatic.UI.Services {
	public class TeamDataService : ITeamDataService {
		private const string RequestUri = "api/v1/teams";
		private readonly HttpClient httpClient;
		//private readonly ITokenService tokenService;

		public TeamDataService(HttpClient httpClient) {//, ITokenService tokenService
			this.httpClient = httpClient;
			//this.tokenService = tokenService;
		}

		public async Task<ApiResponse<TeamDto>> CreateTeamAsync(TeamViewModel teamViewModel) {
			//httpClient.DefaultRequestHeaders.Authorization
			//	= new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
			var result = await httpClient.PostAsJsonAsync(RequestUri, teamViewModel);
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
			var result = await httpClient.GetAsync($"{RequestUri}/user/{userId}", HttpCompletionOption.ResponseHeadersRead);
			result.EnsureSuccessStatusCode();
			var content = await result.Content.ReadAsStringAsync();
			if (!result.IsSuccessStatusCode) {
				throw new ApplicationException(content);
			}
			var teams = JsonSerializer.Deserialize<List<TeamDto>>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
			return teams!;
		}
	}
}