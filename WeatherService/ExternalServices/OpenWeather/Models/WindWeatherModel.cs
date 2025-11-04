namespace WeatherService.ExternalServices.OpenWeather.Models;

public sealed record WindWeatherModel
{
    public required float Speed { get; init; }
}
