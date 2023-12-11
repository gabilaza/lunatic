
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Domain.Utils;


namespace Lunatic.Infrastructure.Repositories {
    public class UserRepository : RepositoryBase<User>, IUserRepository {
        public UserRepository(LunaticContext context) : base(context) {
        }

        public async Task<bool> ExistsByUsernameAsync(string username) {
            var result = await FindByUsernameAsync(username);
            return result.IsSuccess;
        }

        public async Task<bool> ExistsByEmailAsync(string email) {
            var result = await FindByEmailAsync(email);
            return result.IsSuccess;
        }

        public async Task<Result<User>> FindByUsernameAsync(string username) {
            var result = await context.Set<User>().FindAsync(username);
            if(result == null) {
                return Result<User>.Failure($"Entity with username {username} not found");
            }
            return Result<User>.Success(result);
        }

        public async Task<Result<User>> FindByEmailAsync(string email) {
            var result = await context.Set<User>().FindAsync(email); //cautare cu predicat + extensie ; first or default + predicat 
            if(result == null) {
                return Result<User>.Failure($"Entity with username {email} not found");
            }
            return Result<User>.Success(result);
        }
    }
}

