namespace WeatherService.ExternalServices.OpenWeather.Models;

public sealed record GeographicalCoordinatesModel
{
    public float Lat { get; init; }
    public float Lon { get; init; }
}
