using Microsoft.Extensions.Logging;

internal class CustomApiClient(HttpClient client, ILogger<CustomApiClient> logger) : IDisposable
{
    private readonly HttpClient _client = client;
    private readonly ILogger<CustomApiClient> _logger = logger;

    public void Dispose() => _client.Dispose();

    public async Task MakeApiCall()
    {
        using var message = new HttpRequestMessage(HttpMethod.Get, "https://statuscodes.com/200");
        message.Headers.Add("customHeader", "customValue");
        using var response = await _client.SendAsync(message);
        _logger.LogInformation(eventId: 3, "Got response code {StatusCode}", response.StatusCode);
    }
}