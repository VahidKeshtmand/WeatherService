using Microsoft.Extensions.Options;
using System.Text.Json;
using WeatherService.ExternalServices.OpenWeather.Models;
using WeatherService.Options;

namespace WeatherService.ExternalServices.OpenWeather;

public class OpenWeatherService : IOpenWeatherService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonSerializerOptions;
    private readonly ILogger<OpenWeatherService> _logger;
    private readonly OpenWeatherServiceOptions _options;

    public OpenWeatherService(
        HttpClient httpClient,
        ILogger<OpenWeatherService> logger,
        IOptions<OpenWeatherServiceOptions> options) {
        _options = options.Value;
        httpClient.BaseAddress = new Uri(_options.BaseUrl);
        httpClient.Timeout = TimeSpan.FromSeconds(_options.RequestTimeoutSeconds);
        _httpClient = httpClient;
        _jsonSerializerOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        _logger = logger;
    }

    public async Task<GeographicalCoordinatesModel[]?> GetGeographicalCoordinatesAsync(string cityName, CancellationToken cancellationToken) {
        var url = $"geo/1.0/direct?q={cityName}&appid={_options.ApiKey}";
        _logger.LogInformation("Requesting coordinates for city '{CityName}' from {Url}", cityName, url);

        try
        {
            var response = await _httpClient.GetAsync(url, cancellationToken);

            _logger.LogInformation("Received response for '{CityName}' with status code {StatusCode}", cityName,
                response.StatusCode);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<GeographicalCoordinatesModel[]>(_jsonSerializerOptions, cancellationToken);

            if ( result is null || result.Length == 0) {
                _logger.LogWarning("Received empty response for city '{CityName}'", cityName);
            }

            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception occured with exception message {message}", ex.Message);
            throw;
        }
    }

    public async Task<WeatherModel?> GetCurrentWeatherAsync(double lat, double lon, CancellationToken cancellationToken) {
        var url = $"data/2.5/weather?lat={lat}&lon={lon}&appid={_options.ApiKey}";
        _logger.LogInformation("Requesting weather for lat '{lat}' and lon '{lon}' from {Url}", lat, lon, url);

        try {
            var response = await _httpClient.GetAsync(url, cancellationToken);

            _logger.LogInformation("Received response for lat '{lat}' and lon '{lon}' with status code {StatusCode}", lat, lon, response.StatusCode);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<WeatherModel>(_jsonSerializerOptions, cancellationToken);

            if ( result is null ) {
                _logger.LogWarning("Received empty response for lat '{lat}' and lon '{lon}'", lat, lon);
            }

            return result;
        } catch ( Exception ex ) {
            _logger.LogError(ex, "Exception occured with exception message {message}", ex.Message);
            throw;
        }
    }

    public async Task<AirPollutionModel?> GetCurrentAirPollutionAsync(double lat, double lon, CancellationToken cancellationToken) {
        var url = $"data/2.5/air_pollution?lat={lat}&lon={lon}&appid={_options.ApiKey}";
        _logger.LogInformation("Requesting air pollution for lat '{lat}' and lon '{lon}' from {Url}", lat, lon, url);

        try {
            var response = await _httpClient.GetAsync(url, cancellationToken);

            _logger.LogInformation("Received response for lat '{lat}' and lon '{lon}' with status code {StatusCode}", lat, lon, response.StatusCode);

            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<AirPollutionModel>(_jsonSerializerOptions, cancellationToken);

            if ( result is null ) {
                _logger.LogWarning("Received empty response for lat '{lat}' and lon '{lon}'", lat, lon);
            }

            return result;
        } catch ( Exception ex ) {
            _logger.LogError(ex, "Exception occured with exception message {message}", ex.Message);
            throw;
        }
    }
}
