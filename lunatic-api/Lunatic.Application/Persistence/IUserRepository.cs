
using Lunatic.Domain.Entities;
using Lunatic.Domain.Utils;


namespace Lunatic.Application.Persistence {
    public interface IUserRepository : IAsyncRepository<User> {
        Task<Result<User>> FindByUsernameAsync(string username);
        Task<Result<User>> FindByEmailAsync(string email);
    }
}

