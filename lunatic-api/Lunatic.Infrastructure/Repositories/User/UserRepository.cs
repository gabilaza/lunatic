
using Lunatic.Application.Persistence;
using Lunatic.Domain.Entities;
using Lunatic.Domain.Utils;
using Microsoft.EntityFrameworkCore;


namespace Lunatic.Infrastructure.Repositories {
    public class UserRepository : RepositoryBase<User>, IUserRepository {
        public UserRepository(LunaticContext context) : base(context) {
        }

        public async Task<Result<User>> FindByUsernameAsync(string username) {
			var result = await context.Set<User>().FirstOrDefaultAsync(u => u.Username == username);

			if (result == null) {
				return Result<User>.Failure($"Entity with username {username} not found");
			}
			return Result<User>.Success(result);
        }

        public async Task<Result<User>> FindByEmailAsync(string email) {
			var result = await context.Set<User>().FirstOrDefaultAsync(u => u.Email == email);

			if (result == null) {
				return Result<User>.Failure($"Entity with username {email} not found");
			}
			return Result<User>.Success(result);
		}
    }
}

