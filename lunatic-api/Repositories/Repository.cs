
using Utils;

namespace Repositories {
    public interface Repository<T> where T : class {
        Task<Result<T>> FindById(Guid id);
        Task<Result<T>> Add(T entity);
        Task<Result<T>> Update(T entity);
        Task<Result<T>> Delete(Guid id);

        Task<Result<IReadOnlyList<T>>> GetPagedReponse(int page, int size);
    }
}

