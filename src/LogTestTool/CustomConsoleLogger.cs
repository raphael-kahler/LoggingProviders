using Microsoft.Extensions.Logging;

internal sealed class CustomConsoleLogger(string categoryName) : ILogger
{
    private string _categoryName = categoryName;

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull => default;

    public bool IsEnabled(LogLevel logLevel)
    {
        // Console.WriteLine($"... checking if custom console logger is enabled for log level {logLevel}");
        return true;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        ConsoleColor originalForegroundColor = Console.ForegroundColor;
        ConsoleColor originalBackgroundColor = Console.BackgroundColor;

        Console.ForegroundColor = ConsoleColor.Blue;
        Console.BackgroundColor = ConsoleColor.Black;

        Console.WriteLine($"{_categoryName}[{eventId}]: {logLevel,-12}");
        Console.WriteLine($"      {formatter(state, exception)}");

        Console.ForegroundColor = originalForegroundColor;
        Console.BackgroundColor = originalBackgroundColor;

    }
}
