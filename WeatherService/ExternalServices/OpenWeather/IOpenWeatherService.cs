using WeatherService.ExternalServices.OpenWeather.Models;

namespace WeatherService.ExternalServices.OpenWeather;

public interface IOpenWeatherService
{
    Task<GeographicalCoordinatesModel[]?> GetGeographicalCoordinatesAsync(string cityName,
        CancellationToken cancellationToken);

    Task<WeatherModel?> GetCurrentWeatherAsync(double lat, double lon, CancellationToken cancellationToken);

    Task<AirPollutionModel?> GetCurrentAirPollutionAsync(double lat, double lon, CancellationToken cancellationToken);
}