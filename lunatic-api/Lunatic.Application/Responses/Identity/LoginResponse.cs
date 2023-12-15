

namespace Lunatic.Application.Responses.Identity {
    public class LoginResponse : ResponseBase {
        public LoginResponse() : base() { }

        public string Token { get; set; } = default!;
        public string UserId { get; set; } = default!;
    }
}
