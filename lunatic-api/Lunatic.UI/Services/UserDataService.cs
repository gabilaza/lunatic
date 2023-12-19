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

		public async Task<List<UserDto>> GetUsersByUsernameAsync(string usernameMatch) {
			//httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await tokenService.GetTokenAsync());

			//var response = await JsonSerializer.DeserializeAsync<List<UserDto>>(
			//	await httpClient.GetStreamAsync($"{RequestUri}/usernames/{usernameMatch}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
			//	);

			var response = JsonSerializer.Deserialize<List<UserDto>>(
				"""
				[{"UserId":"c8a34daa-4946-4bc3-aaa2-d4653d5c36e8","CreatedDate":"2023-12-18T23:02:55.595286+02:00","FirstName":"filip","LastName":"Valen","Email":"fv@a.a","Username":"filipv","Password":"qwertyQ1234-","Role":1,"TeamIds":["7bca037e-74b4-4fb1-9207-5f86a3a532af"]}, {"UserId":"72b2e40a-5fb4-4f00-9c3a-cbcb8dd71e69","CreatedDate":"2023-12-19T22:14:30.055316+02:00","FirstName":"string","LastName":"string","Email":"user1@example.com","Username":"mihai","Password":"qwerty12345-Q","Role":1,"TeamIds":[]}, {"UserId":"095ebfc0-9cc3-414f-865c-d1869b686208","CreatedDate":"2023-12-19T22:14:54.245861+02:00","FirstName":"string","LastName":"string","Email":"user2@example.com","Username":"meehi","Password":"qwerty12345-Q","Role":1,"TeamIds":[]}, {"UserId":"b29255c8-05d4-44a3-826b-7b848e67d71a","CreatedDate":"2023-12-19T22:15:08.867163+02:00","FirstName":"string","LastName":"string","Email":"user3@example.com","Username":"mimi","Password":"qwerty12345-Q","Role":1,"TeamIds":[]}, {"UserId":"a180b871-7db3-4aee-bf0a-9a83a93df614","CreatedDate":"2023-12-19T22:17:06.839701+02:00","FirstName":"string","LastName":"string","Email":"user5@example.com","Username":"dumi","Password":"qwerty12345-Q","Role":1,"TeamIds":[]}, {"UserId":"67023413-1566-41da-9717-f23049249d16","CreatedDate":"2023-12-19T22:17:16.383342+02:00","FirstName":"string","LastName":"string","Email":"user6@example.com","Username":"juni","Password":"qwerty12345-Q","Role":1,"TeamIds":[]}]
				""",
				new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }
				);

			//return response!;
			return response;
		}

		public Task<ApiResponse<bool>> RemoveUserFromTeamAsync(string memberId, string teamId) {

		}

		//public async Task UpdateUserAsync(UserDto user) {
		//	httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await tokenService.GetTokenAsync());
		//	var userJson = new StringContent(JsonSerializer.Serialize(user), Encoding.UTF8, "application/json");
		//	await httpClient.PutAsync("api/users", userJson);
		//}
	}
}
