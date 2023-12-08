using Lunatic.Domain.Models;
using Lunatic.Domain.Utils;


namespace Lunatic.Application.Persistence {
    public interface IUserRepository : IAsyncSRepository<User> {
        Task<Result<User>> FindByUsernameAsync(string username);
        Task<Result<User>> FindByEmailAsync(string email);
    }
}

