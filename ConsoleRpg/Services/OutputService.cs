using ConsoleRpg.Helpers;
using ConsoleRpgEntities.Services;

namespace ConsoleRpg.Services;

public class OutputService : IOutputService
{
    private readonly OutputManager _outputManager;

    public OutputService(OutputManager outputManager)
    {
        _outputManager = outputManager;
    }

    public void Write(string message)
    {
        _outputManager.AddLogEntry(message);
    }

    public void WriteLine(string message)
    {
        _outputManager.AddLogEntry(message); // could add additional newlines here, for example, "\n"
    }
}