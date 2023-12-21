
using Lunatic.Application.Models.Identity;
using Lunatic.Application.Responses.Identity;


namespace Lunatic.Application.Contracts.Identity {
    public interface IAuthService {
        Task<RegisterResponse> Registeration(RegistrationModel model, string role);
        Task<LoginResponse> Login(LoginModel model);
        Task<(int, string)> Logout();
	}
}
