
using Lunatic.Application.Persistence;
using Lunatic.Domain.Utils;
using Microsoft.EntityFrameworkCore;


namespace Lunatic.Infrastructure.Repositories {
    public class BaseSRepository<T> : IAsyncSRepository<T> where T : class {
        protected readonly LunaticContext context;

        public BaseSRepository(LunaticContext context) {
            this.context = context;
        }

        public virtual async Task<Result<T>> FindByIdAsync(string id) {
            var result = await context.Set<T>().FindAsync(id);
            if (result == null) {
                return Result<T>.Failure($"Entity with id {id} not found");
            }
            return Result<T>.Success(result);
        }

        public virtual async Task<Result<T>> AddAsync(T entity) {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }

        public virtual async Task<Result<T>> UpdateAsync(T entity) {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }

        public virtual async Task<Result<T>> DeleteAsync(string id) {
            var result = await FindByIdAsync(id);
            if (result.Value != null) {
                context.Set<T>().Remove(result.Value);
                await context.SaveChangesAsync();
                return Result<T>.Success(result.Value);
            }
            return Result<T>.Failure($"Entity with id {id} not found");
        }

        public virtual async Task<Result<IReadOnlyList<T>>> GetPagedResponseAsync(int page, int size) {
            var result = await context.Set<T>().Skip(page).Take(size).AsNoTracking().ToListAsync();
            return Result<IReadOnlyList<T>>.Success(result);
        }

        public virtual async Task<Result<IReadOnlyList<T>>> GetAllAsync() {
            var result = await context.Set<T>().AsNoTracking().ToListAsync();
            return Result<IReadOnlyList<T>>.Success(result);
        }
    }

}

