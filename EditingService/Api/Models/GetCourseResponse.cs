namespace Api.Models;

public record GetCourseResponse
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string Description { get; init; }
    
    public required Participant[] Participants { get; init; }
}

public abstract record Participant
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
}