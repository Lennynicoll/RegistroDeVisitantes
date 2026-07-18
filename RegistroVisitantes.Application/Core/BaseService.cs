namespace RegistroVisitantes.Application.Core
{
    public abstract class BaseService<T> : IBaseService<T> where T : class
    {
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Task.FromResult<IEnumerable<T>>(Enumerable.Empty<T>());
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            return await Task.FromResult<T?>(null);
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            return await Task.FromResult(entity);
        }

        public virtual async Task<T?> UpdateAsync(int id, T entity)
        {
            return await Task.FromResult<T?>(null);
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            return await Task.FromResult(false);
        }
    }
}
