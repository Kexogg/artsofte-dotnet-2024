namespace Core.TraceLogic.Interfaces;

/// <summary>
/// чтение трассировочных значений при создании нового scoped
///
///  // для HTTP мы создаем middleware и  в нем это делаем 
/// </summary>
public interface ITraceReader
{
    string Name { get; }

    string GetValue();
}