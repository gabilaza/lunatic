using Blazored.LocalStorage;
using GlobalBuyTicket.App.Services;
using Lunatic.UI.Contracts;
using Lunatic.UI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lunatic.UI {
	public class Program {
		public static async Task Main(string[] args) {
			var builder = WebAssemblyHostBuilder.CreateDefault(args);
			builder.RootComponents.Add<App>("#app");
			builder.RootComponents.Add<HeadOutlet>("head::after");

			builder.Services.AddAuthorizationCore();
			builder.Services.AddBlazoredLocalStorage(config => {
				config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
				config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
				config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
				config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
				config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
				config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
				config.JsonSerializerOptions.WriteIndented = false;
			});
			builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });


			builder.Services.AddScoped<ITokenService, TokenService>();
			//builder.Services.AddScoped<CustomStateProvider>();
			builder.Services.AddHttpClient<ITeamDataService, TeamDataService>(client => {
				client.BaseAddress = new Uri("https://localhost:7555/");
			});
			//builder.Services.AddScoped<AuthenticationStateProvider>(s => s.GetRequiredService<CustomStateProvider>());
			//builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client => {
			//	client.BaseAddress = new Uri("https://localhost:7165/");
			//});

			builder.Services.AddMudServices();

			await builder.Build().RunAsync();

		}
	}
}
