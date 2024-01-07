using Lunatic.UI.Contracts;
using Lunatic.UI.Models.Dtos;
using Lunatic.UI.Models.ViewModels;
using Lunatic.UI.Services.Responses;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Lunatic.UI.Services {
	public class ProjectDataService : IProjectDataService {
		private const string RequestUri = "api/v1/projects";
		private readonly HttpClient httpClient;
		private readonly ITokenService tokenService;

		public ProjectDataService(HttpClient httpClient, ITokenService tokenService) {
			this.httpClient = httpClient;
			this.tokenService = tokenService;
		}

		public async Task<ApiResponse> AddSectionAsync(string projectId, string sectionTitle) {
			var result = await httpClient.PostAsJsonAsync($"api/v1/projects/{projectId}/sections", sectionTitle);
			var response = await result.Content.ReadFromJsonAsync<ApiResponse>();
			response!.Success = result.IsSuccessStatusCode;
			return response!;
		}

		public async Task<ApiResponse<ProjectDto>> CreateProjectAsync(ProjectViewModel projectViewModel) {
			httpClient.DefaultRequestHeaders.Authorization
				= new AuthenticationHeaderValue("Bearer", await tokenService.GetTokenAsync());
			var result = await httpClient.PostAsJsonAsync(RequestUri, projectViewModel);
			var response = await result.Content.ReadFromJsonAsync<ApiResponse<ProjectDto>>();
			response!.Success = result.IsSuccessStatusCode;
			return response!;
		}


		public Task<ApiResponse<ProjectDto>> DeleteProjectAsync(Guid id) {
			throw new NotImplementedException();
		}

		public async Task<ApiResponse> DeleteSectionAync(string projectId, string sectionId) {
			var result = await httpClient.DeleteAsync($"api/v1/projects/{projectId}/sections/{sectionId}"); // TODO! check if PATCH solves this more easily
			var response = await result.Content.ReadFromJsonAsync<ApiResponse>();
			response!.Success = result.IsSuccessStatusCode;
			return response!;
		}

		public async Task<ApiResponse<ProjectDto>> GetProjectByIdAsync(string teamId) {
			var result = await httpClient.GetAsync($"api/v1/teams/projects/{teamId}", HttpCompletionOption.ResponseHeadersRead);
			var response = await result.Content.ReadFromJsonAsync<ApiResponse<ProjectDto>>();
			response!.Success = result.IsSuccessStatusCode;
			return response!;
		}

		public async Task<ApiResponse<List<TaskDto>>> GetProjectTasksAsync(Guid projectId) {
			var result = await httpClient.GetAsync($"api/v1/projects/{projectId}/tasks", HttpCompletionOption.ResponseHeadersRead);
			var response = await result.Content.ReadFromJsonAsync<ApiResponse<List<TaskDto>>>();
			response!.Success = result.IsSuccessStatusCode;
			return response!;
		}

		public Task<ApiResponse<ProjectDto>> UpdateProjectAsync(ProjectViewModel Project) {
			throw new NotImplementedException();
		}

	}
}
