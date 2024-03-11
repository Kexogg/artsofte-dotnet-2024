namespace Api.Models;

public record CreateSubjectRequest
{
    public required string Name { get; init; }
    public required string Description { get; init; }
}