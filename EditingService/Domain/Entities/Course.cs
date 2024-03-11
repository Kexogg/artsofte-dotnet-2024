using Core.Dal.Base;
using Domain.Interfaces;

namespace Domain.Entities;

/// <summary>
/// Модель курса
/// </summary>
public record Course : BaseEntity<Guid>
{
    
    public required string Name { get; init; }
    
    public required string Description { get; init; }
    
    public Event[]? Events { get; init; }
}