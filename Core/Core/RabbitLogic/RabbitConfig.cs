namespace Core.RabbitLogic;

/// <summary>
/// Конфигурация RabbitMQ
/// </summary>
public record RabbitConfig
{
    public string UserName { get; set; } = GetEnvVarOrDefault("RABBITMQ_USERNAME", "guest");
    public string Password { get; set; } = GetEnvVarOrDefault("RABBITMQ_PASSWORD", "guest");
    public string HostName { get; set; } = GetEnvVarOrDefault("RABBITMQ_HOSTNAME", "localhost");
    public int Port { get; set; } = int.Parse(GetEnvVarOrDefault("RABBITMQ_PORT", "5672"));
    public string VHost { get; set; } = GetEnvVarOrDefault("RABBITMQ_VHOST", "/");
    public int MaxChannels { get; set; } = int.Parse(GetEnvVarOrDefault("RABBITMQ_MAX_CHANNEL_COUNT", "10"));

    private static string GetEnvVarOrDefault(string varName, string defaultValue)
    {
        return Environment.GetEnvironmentVariable(varName) ?? defaultValue;
    }
}