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

    public Guid[]? Participants { get; init; }
}

public record CourseWithParticipants : Course
{
    public required Participant[] Participants { get; set; }
}

public record Participant
{
    public required Guid Id { get; init; }

    public required string Name { get; init; }
}