using WeatherService.ExternalServices.OpenWeather.Models;

namespace WeatherService.Features.CityEnvironmentalCondition;

public sealed class CityEnvironmentalConditionDto
{
    public float Temperature { get; init; }
    public int Humidity { get; init; }
    public float WindSpeed { get; init; }
    public int AirQualityIndex { get; init; }
    public required AirPollutionComponentsModel MajorPollutants { get; init; }
    public required GeographicalCoordinatesModel GeographicalCoordinates { get; init; }
}
