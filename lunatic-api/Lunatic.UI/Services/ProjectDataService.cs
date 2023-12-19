using Lunatic.UI.Contracts;
using Lunatic.UI.Services.Responses;
using Lunatic.UI.ViewModels;
using System.Net.Http.Json;

namespace Lunatic.UI.Services {
	public class ProjectDataService : IProjectDataService {
		private const string RequestUri = "api/v1/teams/projects";
		private readonly HttpClient httpClient;
		//private readonly ITokenService tokenService;

		public ProjectDataService(HttpClient httpClient) {//, ITokenService tokenService
			this.httpClient = httpClient;
			//this.tokenService = tokenService;
		}

		public Task<ApiResponse<ProjectDto>> CreateProjectAsync(ProjectViewModel Project) {
			throw new NotImplementedException();
		}


		public Task<ApiResponse<ProjectDto>> DeleteProjectAsync(Guid id) {
			throw new NotImplementedException();
		}

		public async Task<ProjectDto> GetProjectByIdAsync(string teamId) {
			var result = await httpClient.GetAsync($"{RequestUri}/{teamId}", HttpCompletionOption.ResponseHeadersRead);
			result.EnsureSuccessStatusCode();
			var response = await result.Content.ReadFromJsonAsync<ProjectDto>();
			return response;
		}

		public Task<ApiResponse<ProjectDto>> UpdateProjectAsync(ProjectViewModel Project) {
			throw new NotImplementedException();
		}

	}
}
