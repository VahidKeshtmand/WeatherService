using System.Text.Json.Serialization;

namespace WeatherService.ExternalServices.OpenWeather.Models;

public sealed record AirPollutionComponentsModel
{
    public float CO { get; init; }
    public float NO { get; init; }
    [JsonPropertyName("no2")]
    public float NO2 { get; init; }
    public float O3 { get; init; }
    [JsonPropertyName("so2")]
    public float SO2 { get; init; }
    [JsonPropertyName("pm2_5")]
    public float PM2_5 { get; init; }
    [JsonPropertyName("pm10")]
    public float PM10 { get; init; }
    [JsonPropertyName("nh3")]
    public float NH3 { get; init; }
}
