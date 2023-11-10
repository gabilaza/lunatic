
using Microsoft.EntityFrameworkCore;
using Lunatic.Domain.Utils;


namespace Lunatic.Infrastructure.Repositories {
    public class AsyncRepository<T> : Repository<T> where T : class {
        private readonly LunaticContext context;

        public AsyncRepository(LunaticContext context) {
            this.context = context;
        }

        public async Task<Result<T>> FindById(Guid id) {
            var result = await context.Set<T>().FindAsync(id);
            if (result == null) {
                return Result<T>.Failure($"Entity with id {id} not found");
            }
            return Result<T>.Success(result);
        }

        public async Task<Result<T>> Add(T entity) {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }

        public async Task<Result<T>> Update(T entity) {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
            return Result<T>.Success(entity);
        }

        public async Task<Result<T>> Delete(Guid id) {
            var result = await FindById(id);
            if (result != null) {
                context.Set<T>().Remove(result.Value);
                await context.SaveChangesAsync();
                return Result<T>.Success(result.Value);
            }
            return Result<T>.Failure($"Entity with id {id} not found");
        }

        public async Task<Result<IReadOnlyList<T>>> GetPagedReponse(int page, int size) {
            var result = await context.Set<T>().Skip(page).Take(size).AsNoTracking().ToListAsync();
            return Result<IReadOnlyList<T>>.Success(result);
        }
    }

}

