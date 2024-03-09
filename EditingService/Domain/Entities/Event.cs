using Core.Dal.Base;

namespace Domain.Entities;

/// <summary>
/// Модель события
/// </summary>
public record Event : BaseEntity<Guid>
{
    public required string Name { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
    public required string Description { get; init; }
    public required string Location { get; init; }
}