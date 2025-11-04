namespace WeatherService.Options;

public sealed record OpenWeatherServiceOptions
{
    public required string BaseUrl { get; init; }
    public required string ApiKey { get; init; }
    public int RequestTimeoutSeconds { get; init; }
}
