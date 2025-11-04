namespace WeatherService.ExternalServices.OpenWeather.Models;

public sealed record WeatherModel
{
    public required MainWeatherModel Main { get; init; }
    public required WindWeatherModel Wind { get; init; }
}
