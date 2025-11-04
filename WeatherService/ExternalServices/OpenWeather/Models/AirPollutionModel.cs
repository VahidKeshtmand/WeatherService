namespace WeatherService.ExternalServices.OpenWeather.Models;

public sealed record AirPollutionModel
{
    public required AirPollutionListModel[] List { get; set; }
}
