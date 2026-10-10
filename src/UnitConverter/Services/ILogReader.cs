using UnitConverter.Models;

public interface ILogReader
{
    IEnumerable<LogEntry> Read();
}
