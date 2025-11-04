using System.Text.Json.Serialization;

namespace WeatherService.ExternalServices.OpenWeather.Models;

public sealed record MainWeatherModel
{
    [JsonPropertyName("temp")]
    public float Temperature { get; init; }
    public int Humidity { get; init; }
}
