using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lunatic.UI.Services.Responses {
	public class ApiResponse<T> {
		public string Message { get; set; } = string.Empty;
		public bool Success { get; set; }
		public string? ValidationErrors { get; set; }

		[JsonExtensionData]
		public IDictionary<string, JsonElement>? Data { get; set; }

		private readonly static JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
		public T GetValue(string propertyName) {
			if (Data is null) {
				throw new InvalidOperationException("Data is null");
			}

			return Data[propertyName].Deserialize<T>(options)!;
		}

		public override string ToString() {
			return $"ApiResponse(Message: {Message}, Success: {Success}, ValidationErrors: {ValidationErrors}, Data: {string.Join(Environment.NewLine, Data)})";
		}
	}
}
