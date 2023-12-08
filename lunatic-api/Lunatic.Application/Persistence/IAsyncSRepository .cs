
using Lunatic.Domain.Utils;

namespace Lunatic.Application.Persistence {
    public interface IAsyncSRepository<T> where T : class {
        Task<Result<T>> FindByIdAsync(string id);
        Task<Result<T>> AddAsync(T entity);
        Task<Result<T>> UpdateAsync(T entity);
        Task<Result<T>> DeleteAsync(string id);

        Task<Result<IReadOnlyList<T>>> GetPagedResponseAsync(int page, int size);
        Task<Result<IReadOnlyList<T>>> GetAllAsync();
    }
}

