namespace Api.Models;

public record GetEventResponse
{
    public required Guid Id { get; init; }
    
    public required string Name { get; init; }
    
    public required string Description { get; init; }
    
    public required DateTime StartDate { get; init; }
    
    public required DateTime EndDate { get; init; }
}