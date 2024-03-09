using System.Collections.Concurrent;
using Core.Dal.Base;
using Domain.Interfaces;

namespace Infrastructure.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity<Guid>
{
    private static readonly ConcurrentDictionary<Guid, T> Entities = new();

    public Task<bool> ExistsAsync(Guid id)
    {
        return Task.FromResult(Entities.ContainsKey(id));
    }

    public Task<Guid> CreateAsync(T entity)
    {
        var id = Guid.NewGuid();
        var newEntity = entity with { Id = id };
        Entities.TryAdd(id, newEntity);
        return Task.FromResult(id);
    }

    public Task<T[]> GetAllAsync()
    {
        return Task.FromResult(Entities.Values.ToArray());
    }

    public Task<T?> GetByIdAsync(Guid id)
    {
        return Task.FromResult(Entities.GetValueOrDefault(id));
    }

    public Task DeleteAsync(Guid entity)
    {
        Entities.TryRemove(entity, out _);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(T entity)
    {
        Entities.TryRemove(entity.Id, out _);
        return Task.CompletedTask;
    }

    public Task<T> UpdateAsync(T entity)
    {
        Entities[entity.Id] = entity;
        return Task.FromResult(entity);
    }
}