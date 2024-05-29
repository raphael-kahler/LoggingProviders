using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

internal class ConsoleListener(
    ILogger<ConsoleListener> logger,
    IServiceProvider serviceProvider,
    IHostApplicationLifetime appLifetime) : IHostedService
{
    private readonly ILogger<ConsoleListener> _logger = logger;
    private readonly IServiceProvider _serviceProvider = serviceProvider;
    private readonly IHostApplicationLifetime _appLifetime = appLifetime;
    private readonly CancellationTokenSource _tokenSource = new();
    private Task listenTask = Task.CompletedTask;

    public Task StartAsync(CancellationToken cancellationToken)
    {
        listenTask = Task.Run(DoWork, cancellationToken);
        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _logger.LogWarning("Console listener was cancelled");
        await _tokenSource.CancelAsync();
    }

    private async Task DoWork()
    {
        while (!_tokenSource.IsCancellationRequested)
        {
            Console.WriteLine("Type a command:");
            var input = Console.ReadLine();

            if (input == "quit")
            {
                _appLifetime.StopApplication();
                _tokenSource.Cancel();
            }
            else if (input == "error")
            {
                _logger.LogError(eventId: 2, "Error requested!");
            }
            else if (input == "http")
            {
                using var scope = _serviceProvider.CreateScope();
                using var client = scope.ServiceProvider.GetRequiredService<CustomApiClient>();
                await client.MakeApiCall();
            }
            else
            {
                _logger.LogInformation(eventId: 1, "User said {Message}", input);
            }
        }
    }
}
