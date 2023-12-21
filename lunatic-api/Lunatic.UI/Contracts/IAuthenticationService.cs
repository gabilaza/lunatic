using Lunatic.UI.ViewModels;

namespace Lunatic.UI.Contracts
{
    public interface IAuthenticationService
    {
        Task Login(LoginModel loginRequest);
        Task Register(RegisterModel registerRequest);
        Task Logout();
    }
}
