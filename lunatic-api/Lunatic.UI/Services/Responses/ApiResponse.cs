namespace Lunatic.UI.Services.Responses {
    public class ApiResponse<T> {
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public string? ValidationErrors { get; set; }
        public T? Data { get; set; }
    }
}
