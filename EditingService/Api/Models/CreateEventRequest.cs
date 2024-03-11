namespace Api.Models;

public record CreateEventRequest
{
    public required string Name { get; init; }
    
    public required string Description { get; init; }
    
    public required DateTime StartDate { get; init; }
    
    public required DateTime EndDate { get; init; }
    public required string Location { get; set; }
}