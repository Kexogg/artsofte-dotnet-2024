namespace Api.Controllers.User.Responses;

/// <summary>
/// Ответ с информацией о созданном пользователе
/// </summary>
public record CreateUserResponse
{
    public required Guid Id { get; init; }
}