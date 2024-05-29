using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var hostBuilder = Host.CreateApplicationBuilder(args);
hostBuilder.Services.AddHostedService<ConsoleListener>();
hostBuilder.Services.AddHttpClient<CustomApiClient>();
hostBuilder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, CustomConsoleLoggerProvider>());

using var host = hostBuilder.Build();
await host.RunAsync();

Console.WriteLine("Quitting...");
