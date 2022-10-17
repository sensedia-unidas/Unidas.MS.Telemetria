global using System;
global using System.Linq;
global using System.Net;
global using Microsoft.AspNetCore.Mvc;
global using Newtonsoft.Json;
using Asp.Versioning;
using Asp.Versioning.Conventions;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SensidiaTemplateDotNet.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader =     ApiVersionReader.Combine(
        new HeaderApiVersionReader("Api-Version"),
        new QueryStringApiVersionReader("Query-String-Version"));
});




builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(DomainExceptionFilter));
    options.Filters.Add(typeof(ValidateModelAttribute));
});


builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
});

var app = builder.Build();

var versionSet = app.NewApiVersionSet()
                    .HasApiVersion(1.0)
                    .HasApiVersion(2.0)
                    .ReportApiVersions()
                    .Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();


}

app.UseHttpsRedirection();



app.MapGet("/GetMessage", () => "This is an example of a minimal API").WithApiVersionSet(versionSet).MapToApiVersion(1.0);
app.MapGet("/GetMessage2", () => "This is an example of a minimal API 2").WithApiVersionSet(versionSet).MapToApiVersion(2.0);
app.MapGet("/GetText", () => "This is yet another example of a minimal API").WithApiVersionSet(versionSet).WithApiVersionSet(versionSet).IsApiVersionNeutral();

// GET /weatherforecast?api-version=1.0

app.MapGet("/testLog", () =>
{
    app.Logger.LogCritical("TESTE CRITICO Primeiro Teste page visited at {DT}",
            DateTime.UtcNow.ToLongTimeString());



    app.Logger.LogCritical($"Teste de Log 5 at {DateTime.UtcNow.ToLongTimeString()}");
    app.Logger.LogDebug($"Teste de Log 6 at {DateTime.UtcNow.ToLongTimeString()}");
    app.Logger.LogWarning($"Teste de Log 7 at {DateTime.UtcNow.ToLongTimeString()}");
    app.Logger.LogError($"Teste de Log 8 at {DateTime.UtcNow.ToLongTimeString()}");
    return "ok";
})
.WithName("GetTest");



var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

internal record Car(string nome)
{


}