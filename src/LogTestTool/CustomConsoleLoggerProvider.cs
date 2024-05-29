using Microsoft.Extensions.Logging;

[ProviderAlias("CustomConsole")]
internal sealed class CustomConsoleLoggerProvider : ILoggerProvider
{
    public ILogger CreateLogger(string categoryName)
    {
        Console.WriteLine($"Creating logger for category {categoryName}");
        var logger = new CustomConsoleLogger(categoryName);
        return logger;
    }

    public void Dispose()
    {
    }
}
