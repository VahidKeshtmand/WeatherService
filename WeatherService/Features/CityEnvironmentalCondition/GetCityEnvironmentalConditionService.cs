using WeatherService.Exceptions;
using WeatherService.ExternalServices.OpenWeather;
using WeatherService.ExternalServices.OpenWeather.Models;

namespace WeatherService.Features.CityEnvironmentalCondition;

public class GetCityEnvironmentalConditionService(IOpenWeatherService openWeatherService)
{
    public async Task<CityEnvironmentalConditionDto> GetCityEnvironmentalConditionAsync(string cityName, CancellationToken cancellationToken)
    {
        var geographicalCoordinate = await GetGeographicalCoordinatesAsync(cityName, cancellationToken);

        var currentWeather = await GetCurrentWeatherAsync(cancellationToken, geographicalCoordinate);

        var currentAirPollution = await GetCurrentAirPollutionAsync(cancellationToken, geographicalCoordinate);

        return new CityEnvironmentalConditionDto
        {
            AirQualityIndex = currentAirPollution.List[0].Main.AirQualityIndex,
            Humidity = currentWeather.Main.Humidity,
            Temperature = currentWeather.Main.Temperature,
            WindSpeed = currentWeather.Wind.Speed,
            GeographicalCoordinates = new GeographicalCoordinatesModel
            {
                Lat = geographicalCoordinate[0].Lat,
                Lon = geographicalCoordinate[0].Lon,
            },
            MajorPollutants = new AirPollutionComponentsModel
            {
                CO = currentAirPollution.List[0].Components.CO,
                NH3 = currentAirPollution.List[0].Components.NH3,
                NO = currentAirPollution.List[0].Components.NO,
                NO2 = currentAirPollution.List[0].Components.NO2,
                O3 = currentAirPollution.List[0].Components.O3,
                PM10 = currentAirPollution.List[0].Components.PM10,
                PM2_5 = currentAirPollution.List[0].Components.PM2_5,
                SO2 = currentAirPollution.List[0].Components.SO2,
            }
        };
    }

    private async Task<AirPollutionModel> GetCurrentAirPollutionAsync(CancellationToken cancellationToken,
        GeographicalCoordinatesModel[] geographicalCoordinate)
    {
        var currentAirPollution = await openWeatherService.GetCurrentAirPollutionAsync(geographicalCoordinate[0].Lat, geographicalCoordinate[0].Lon, cancellationToken);
        if (currentAirPollution is null)
        {
            throw new NotFoundException($"Current air pollution data for lat {geographicalCoordinate[0].Lat} and lon {geographicalCoordinate[0].Lon} not found.");
        }

        return currentAirPollution;
    }

    private async Task<WeatherModel> GetCurrentWeatherAsync(CancellationToken cancellationToken,
        GeographicalCoordinatesModel[] geographicalCoordinate)
    {
        var currentWeather = await openWeatherService.GetCurrentWeatherAsync(geographicalCoordinate[0].Lat, geographicalCoordinate[0].Lon, cancellationToken);
        if (currentWeather is null)
        {
            throw new NotFoundException($"Current weather data for lat {geographicalCoordinate[0].Lat} and lon {geographicalCoordinate[0].Lon} not found.");
        }

        return currentWeather;
    }

    private async Task<GeographicalCoordinatesModel[]> GetGeographicalCoordinatesAsync(string cityName, CancellationToken cancellationToken)
    {
        var geographicalCoordinate = await openWeatherService.GetGeographicalCoordinatesAsync(cityName, cancellationToken);
        if (geographicalCoordinate is null || geographicalCoordinate.Length == 0)
        {
            throw new NotFoundException($"Geographical coordinates for city '{cityName}' not found.");
        }

        return geographicalCoordinate;
    }
}