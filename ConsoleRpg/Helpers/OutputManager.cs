using Spectre.Console;

namespace ConsoleRpg.Helpers;

public class OutputManager
{
    private readonly string _initialMapContent;
    private readonly Layout _layout;
    private readonly Panel _mapPanel;
    private readonly List<string> _logContent;
    private const int MaxLogLines = 15; // Maximum number of visible log lines

    public OutputManager(string initialMapContent = "### Initial Map Area ###")
    {
        _initialMapContent = initialMapContent;
        _logContent = new List<string>();

        // Create the initial map panel
        _mapPanel = new Panel(_initialMapContent)
            .Expand()
            .Border(BoxBorder.Square)
            .Padding(1, 1, 1, 1)
            .Header("Map");

        // Create the layout with two regions
        _layout = new Layout()
            .SplitRows(
                new Layout("Map").Size(10), // Fixed size for the map area
                new Layout("Logs"));       // Flexible size for the log area

        // Set the initial content for each region
        _layout["Map"].Update(_mapPanel);
        _layout["Logs"].Update(CreateLogPanel());
    }

    public void Initialize()
    {
        // Render the initial layout
        //AnsiConsole.Clear();
        AnsiConsole.Cursor.SetPosition(0, 0);
        AnsiConsole.Write(_layout);
    }

    public void AddLogEntry(string logEntry)
    {
        // Add the log entry
        _logContent.Add(logEntry);

        // If the log exceeds the maximum visible lines, scroll
        var visibleLogs = _logContent.Skip(Math.Max(0, _logContent.Count - MaxLogLines)).ToList();

        // Update the Logs region of the layout
        _layout["Logs"].Update(CreateLogPanel(visibleLogs));

        // Re-render the layout
        //AnsiConsole.Clear();
        AnsiConsole.Cursor.SetPosition(0, 0);
        AnsiConsole.Write(_layout);
    }

    public string GetUserInput(string prompt)
    {
        // Add the prompt to the visible logs for display
        var visibleLogs = _logContent.Skip(Math.Max(0, _logContent.Count - MaxLogLines)).ToList();
        visibleLogs.Add($"[yellow]{Markup.Escape(prompt)}[/]");

        // Update the Logs region with the prompt
        _layout["Logs"].Update(CreateLogPanel(visibleLogs));

        // Re-render the layout without clearing the console
        AnsiConsole.Write(_layout);

        // Move the cursor to the end of the log panel
        var cursorTop = Console.CursorTop;
        Console.SetCursorPosition(2, cursorTop); // 2 spaces for padding after the border

        // Display the input prompt and capture user input
        Console.Write("> ");
        var userInput = Console.ReadLine()?.Trim() ?? string.Empty;

        // Log the user's input
        AddLogEntry($"[yellow]User Input:[/] {Markup.Escape(userInput)}");

        return userInput;
    }




    public void UpdateMapContent(string newMapContent)
    {
        // Update the map panel content
        var updatedMapPanel = new Panel(newMapContent)
            .Expand()
            .Border(BoxBorder.Square)
            .Padding(1, 1, 1, 1)
            .Header("Map");

        // Update the Map region of the layout
        _layout["Map"].Update(updatedMapPanel);

        // Re-render the layout
        AnsiConsole.Clear();
        AnsiConsole.Write(_layout);
    }

    private Panel CreateLogPanel(IEnumerable<string> logs = null)
    {
        // Create a panel for the logs
        var logText = string.Join("\n", logs ?? _logContent);

        return new Panel(new Markup(logText))
            .Expand()
            .Border(BoxBorder.Square)
            .Padding(1, 1, 1, 1)
            .Header($"Logs ({_logContent.Count})");
    }
}