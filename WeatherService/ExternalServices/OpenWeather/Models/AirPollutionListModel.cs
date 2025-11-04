namespace WeatherService.ExternalServices.OpenWeather.Models;

public sealed record AirPollutionListModel
{
    public required AirPollutionMainModel Main { get; init; }
    public required AirPollutionComponentsModel Components { get; init; }
}
