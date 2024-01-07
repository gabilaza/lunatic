using Lunatic.UI.Contracts;
using Lunatic.UI.Models.Dtos;
using Lunatic.UI.Services.Responses;
using System.Net.Http.Json;

namespace Lunatic.UI.Services {
	public class TaskDataService : ITaskDataService {
		private const string RequestUri = "api/v1/tasks";
		private readonly HttpClient httpClient;
		private readonly ITokenService tokenService;

		public TaskDataService(HttpClient httpClient, ITokenService tokenService) {
			this.httpClient = httpClient;
			this.tokenService = tokenService;
		}

		public async Task<ApiResponse> AddTaskSectionAsync(string taskId, string taskSection) {
			var result = await httpClient.PostAsJsonAsync($"{RequestUri}/{taskId}/sections", taskSection); //TODO: FIX REQUEST VERB?!
			var response = await result.Content.ReadFromJsonAsync<ApiResponse>();
			response!.Success = result.IsSuccessStatusCode;
			return response!;
		}

		Task<ApiResponse<TaskDto>> ITaskDataService.GetTaskById(string taskId) {
			throw new NotImplementedException();
		}
	}
}
