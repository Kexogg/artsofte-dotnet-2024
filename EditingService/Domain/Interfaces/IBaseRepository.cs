using Core.Dal.Base;

namespace Domain.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity<Guid>
{
    public Task<bool> ExistsAsync(Guid id);
    
    public Task<Guid> CreateAsync(T entity);

    public Task<T[]> GetAllAsync();

    public Task<T?> GetByIdAsync(Guid id);

    public Task DeleteAsync(Guid entity);

    public Task<T> UpdateAsync(T entity);
}