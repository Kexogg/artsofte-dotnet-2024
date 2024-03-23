namespace Core.TraceLogic.Interfaces;

/// <summary>
/// Запись трассировочных значений при отправке запроса
/// </summary>
public interface ITraceWriter
{
    string Name { get; }
    
    void WriteValue(string value);
}