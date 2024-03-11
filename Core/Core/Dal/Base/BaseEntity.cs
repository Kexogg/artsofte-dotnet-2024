namespace Core.Dal.Base;

/// <summary>
/// Базовая сущность
/// </summary>
/// <typeparam name="T">Тип идентификатора</typeparam>
public record BaseEntity<T>
{
    public T? Id { get; set; }
}