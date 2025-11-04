using Microsoft.AspNetCore.Mvc;
using WeatherService;
using WeatherService.ExternalServices.OpenWeather;
using WeatherService.Features.CityEnvironmentalCondition;
using WeatherService.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.Configure<OpenWeatherServiceOptions>(builder.Configuration.GetSection(nameof(OpenWeatherServiceOptions)));
builder.Services.AddScoped<GetCityEnvironmentalConditionService>();
builder.Services.AddHttpClient<IOpenWeatherService, OpenWeatherService>();
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseExceptionHandler();

app.MapGet("api/city-environmental-condition/{cityName}", async (
    [FromRoute] string cityName,
    GetCityEnvironmentalConditionService getCityEnvironmentalConditionService,
    CancellationToken CancellationToken) =>
{
    return Results.Ok(await getCityEnvironmentalConditionService.GetCityEnvironmentalConditionAsync(cityName, CancellationToken));
});

app.Run();
