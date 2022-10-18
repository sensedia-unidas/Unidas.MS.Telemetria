global using System;
global using System.Linq;
global using System.Net;
global using Microsoft.AspNetCore.Mvc;
global using Newtonsoft.Json;
using Asp.Versioning;
using Asp.Versioning.Conventions;
using Asp.Versioning.ApiExplorer;
using Microsoft.ApplicationInsights.AspNetCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SensidiaTemplateDotNet.Filters;
using Microsoft.OpenApi.Models;
using SensidiaTemplateDotNet.Service.Commands.PickUpCar;
using SensidiaTemplateDotNet.Service.Commands.RegisterCar;
using SensidiaTemplateDotNet.Service.Repositores;
using SensidiaTemplateDotNet.Infrastructure.InMemoryDataAcess.Repositories;
using SensidiaTemplateDotNet.UseCases.RegisterCar;
using SensidiaTemplateDotNet.Infrastructure.InMemoryDataAcess;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new OpenApiInfo() { Title = "API V1", Version = "V1.0" });
    //options.SwaggerDoc("V2", new OpenApiInfo() { Title = "API V2", Version = "V2.0" });
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.CustomSchemaIds(x => x.FullName);
});



//REPOSITORY
builder.Services.AddScoped<ICarReadOnlyRepository, CarRepository>();
builder.Services.AddScoped<ICarWriteOnlyRepository, CarRepository>();

//SERVICE
builder.Services.AddScoped<IPickUpCarUseCase, PickUpCarUseCase>();
builder.Services.AddScoped<IRegisterCarUseCase, RegisterUseCase>();

builder.Services.AddSingleton<SensidiaTemplateDotNet.Infrastructure.InMemoryDataAcess.Context>();


builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader =     ApiVersionReader.Combine(
        new HeaderApiVersionReader("Api-Version"),
        new QueryStringApiVersionReader("Api-Version"));
}).EnableApiVersionBinding();




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
                    //.HasApiVersion(2.0)
                    .ReportApiVersions()
                    .Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/V1/swagger.json", "V1.0");
        //options.SwaggerEndpoint($"/swagger/V2/swagger.json", "V2.0");
    }
  );


}

app.UseHttpsRedirection();


//TODO: Versao pronta para versionar, porém o Swagger precisa ser implementado corretamente para separar as versoes e mostrar como chamar cada uma delas
//app.MapGet("/GetMessage", () => "This is an example of a minimal API").WithApiVersionSet(versionSet).MapToApiVersion(1.0);
//app.MapGet("/GetMessage", () => "This is an example of a minimal API 2").WithApiVersionSet(versionSet).MapToApiVersion(2.0);
//app.MapGet("/GetText", () => "This is yet another example of a minimal API").WithApiVersionSet(versionSet).WithApiVersionSet(versionSet).IsApiVersionNeutral();

// GET /weatherforecast?api-version=1.0

app.MapPost("/car/registerAsync", async (RegisterCarRequest request, IRegisterCarUseCase registerCar) =>
{
    app.Logger.LogInformation($"Novo registro de carro solicitado", request);

    var response = await registerCar.Execute(request.Description, request.Plate);

    return response;

});

app.MapGet("/testLog", () =>
{
    app.Logger.LogCritical("TESTE CRITICO Primeiro Teste page visited at {DT}",
            DateTime.UtcNow.ToLongTimeString());



    app.Logger.LogCritical($"Teste de Log 5 at {DateTime.UtcNow.ToLongTimeString()}");
    app.Logger.LogDebug($"Teste de Log 6 at {DateTime.UtcNow.ToLongTimeString()}");
    app.Logger.LogWarning($"Teste de Log 7 at {DateTime.UtcNow.ToLongTimeString()}");
    app.Logger.LogError($"Teste de Log 8 at {DateTime.UtcNow.ToLongTimeString()}");
    return "ok";
}).WithApiVersionSet(versionSet).MapToApiVersion(1.0);



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
