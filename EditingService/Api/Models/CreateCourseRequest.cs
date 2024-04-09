namespace Api.Models;

public record CreateCourseRequest
{
    public required string Name { get; init; }
    
    public required string Description { get; init; }
    
    public required Guid[] Participants { get; init; }
}