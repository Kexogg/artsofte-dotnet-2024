using Core.Dal.Base;

namespace Domain.Entities;

/// <summary>
/// Модель предмета
/// </summary>
public record Subject : BaseEntity<Guid>
{
    public required string Name { get; init; }
    
    public required string Description { get; init; }
    
    public Course[]? Courses { get; init; }
}