using System.Text.Json.Serialization;

namespace WeatherService.ExternalServices.OpenWeather.Models;

public sealed record AirPollutionMainModel
{
    [JsonPropertyName("aqi")]
    public int AirQualityIndex { get; init; }
}
