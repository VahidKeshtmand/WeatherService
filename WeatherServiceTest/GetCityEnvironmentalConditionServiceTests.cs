using Moq;
using WeatherService.Exceptions;
using WeatherService.ExternalServices.OpenWeather;
using WeatherService.ExternalServices.OpenWeather.Models;
using WeatherService.Features.CityEnvironmentalCondition;

namespace WeatherService.Tests;

public class GetCityEnvironmentalConditionServiceTests
{
    private readonly Mock<IOpenWeatherService> _weatherServiceMock;
    private readonly GetCityEnvironmentalConditionService _sut;

    public GetCityEnvironmentalConditionServiceTests()
    {
        _weatherServiceMock = new Mock<IOpenWeatherService>();
        _sut = new GetCityEnvironmentalConditionService(_weatherServiceMock.Object);
    }

    [Fact]
    public async Task GetCityEnvironmentalConditionAsync_ShouldReturnDto_WhenAllDataAvailable()
    {
        // Arrange
        var cityName = "Tehran";

        var coordinates = new[]
        {
            new GeographicalCoordinatesModel { Lat = (float)35.6892, Lon = (float)51.3890 }
        };

        var weather = new WeatherModel
        {
            Main = new MainWeatherModel { Temperature = (float)25.5, Humidity = 60 },
            Wind = new WindWeatherModel { Speed = (float)3.5 }
        };

        var pollution = new AirPollutionModel
        {
            List = [
                new AirPollutionListModel
                {
                    Main = new AirPollutionMainModel { AirQualityIndex = 2 },
                    Components = new AirPollutionComponentsModel
                    {
                        CO = 100, NO2 = 15, O3 = 10, PM10 = 20, PM2_5 = 10,
                        SO2 = 5, NH3 = 3, NO = 2
                    }
                }]
        };

        _weatherServiceMock.Setup(x => x.GetGeographicalCoordinatesAsync(cityName, It.IsAny<CancellationToken>()))
            .ReturnsAsync(coordinates);

        _weatherServiceMock.Setup(x => x.GetCurrentWeatherAsync(coordinates[0].Lat, coordinates[0].Lon, It.IsAny<CancellationToken>()))
            .ReturnsAsync(weather);

        _weatherServiceMock.Setup(x => x.GetCurrentAirPollutionAsync(coordinates[0].Lat, coordinates[0].Lon, It.IsAny<CancellationToken>()))
            .ReturnsAsync(pollution);

        // Act
        var result = await _sut.GetCityEnvironmentalConditionAsync(cityName, CancellationToken.None);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(25.5, result.Temperature);
        Assert.Equal(60, result.Humidity);
        Assert.Equal(2, result.AirQualityIndex);
        Assert.Equal(3.5, result.WindSpeed);
        Assert.Equal((float)35.6892, result.GeographicalCoordinates.Lat);
        Assert.Equal((float)51.3890, result.GeographicalCoordinates.Lon);
    }

    [Fact]
    public async Task GetCityEnvironmentalConditionAsync_ShouldThrowNotFound_WhenCoordinatesNotFound()
    {
        // Arrange
        _weatherServiceMock.Setup(x => x.GetGeographicalCoordinatesAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((GeographicalCoordinatesModel[]?)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _sut.GetCityEnvironmentalConditionAsync("UnknownCity", CancellationToken.None));
    }

    [Fact]
    public async Task GetCityEnvironmentalConditionAsync_ShouldThrowNotFound_WhenWeatherIsNull()
    {
        // Arrange
        var coordinates = new[]
        {
            new GeographicalCoordinatesModel { Lat = 10, Lon = 20 }
        };

        _weatherServiceMock.Setup(x => x.GetGeographicalCoordinatesAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(coordinates);

        _weatherServiceMock.Setup(x => x.GetCurrentWeatherAsync(10, 20, It.IsAny<CancellationToken>()))
            .ReturnsAsync((WeatherModel?)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _sut.GetCityEnvironmentalConditionAsync("XCity", CancellationToken.None));
    }

    [Fact]
    public async Task GetCityEnvironmentalConditionAsync_ShouldThrowNotFound_WhenAirPollutionIsNull()
    {
        // Arrange
        var coordinates = new[]
        {
            new GeographicalCoordinatesModel { Lat = 10, Lon = 20 }
        };
        var weather = new WeatherModel
        {
            Main = new MainWeatherModel() { Temperature = 20, Humidity = 50 },
            Wind = new WindWeatherModel() { Speed = 5 }
        };

        _weatherServiceMock.Setup(x => x.GetGeographicalCoordinatesAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(coordinates);

        _weatherServiceMock.Setup(x => x.GetCurrentWeatherAsync(10, 20, It.IsAny<CancellationToken>()))
            .ReturnsAsync(weather);

        _weatherServiceMock.Setup(x => x.GetCurrentAirPollutionAsync(10, 20, It.IsAny<CancellationToken>()))
            .ReturnsAsync((AirPollutionModel?)null);

        // Act & Assert
        await Assert.ThrowsAsync<NotFoundException>(() =>
            _sut.GetCityEnvironmentalConditionAsync("XCity", CancellationToken.None));
    }
}