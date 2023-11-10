
namespace Lunatic.Application.Responses {
    public class ResponseBase {
        public ResponseBase() => Success = true;

        public ResponseBase(string message, bool success) {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public string Message { get; set; }

        public List<string>? ValidationsErrors { get; set; }
    }
}
