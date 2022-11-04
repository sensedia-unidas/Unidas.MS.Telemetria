global using Microsoft.AspNetCore.Mvc;
global using Newtonsoft.Json;
global using System;
global using System.Linq;
global using System.Net;
using FluentValidation;
using MsSensediaTemplate.API.Helpers;
using MsSensediaTemplate.Application.Interfaces.Commands.PickUpCar;
using MsSensediaTemplate.Application.Interfaces.Commands.RegisterCar;
using MsSensediaTemplate.Application.Interfaces.Commands.PickUpCar;
using MsSensediaTemplate.Application.Interfaces.Commands.RegisterCar;
using MsSensediaTemplate.Application.ViewModels.Car;
using MsSensediaTemplate.Application.ViewModels.Car.Requests;
using MsSensediaTemplate.Infra.IoC;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

#region Configuracoes adicionadas - builder.services
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new OpenApiInfo() { Title = "API V1", Version = "V1.0" });
    //options.SwaggerDoc("V2", new OpenApiInfo() { Title = "API V2", Version = "V2.0" });
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.CustomSchemaIds(x => x.FullName);
});

NativeInjector.RegisterServices(builder.Services);

//builder.Services.AddValidatorsFromAssemblyContaining<Program>();

//builder.Services.AddApplicationInsightsTelemetry(options =>
//{
//    options.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
//});

//builder.Services.AddApiVersioning(options =>
//{
//    options.DefaultApiVersion = new ApiVersion(1, 0);
//    options.ReportApiVersions = true;
//    options.AssumeDefaultVersionWhenUnspecified = true;
//    options.ApiVersionReader = ApiVersionReader.Combine(
//        new HeaderApiVersionReader("Api-Version"),
//        new QueryStringApiVersionReader("Api-Version"));
//}).EnableApiVersionBinding();

builder.Services.AddMvc(options =>
{
    //options.Filters.Add(typeof(DomainExceptionFilter));
    options.Filters.Add(typeof(ValidateActionFilterAttribute));
});
#endregion

var app = builder.Build();

#region Configuracoes adicionadas - app
//var versionSet = app.NewApiVersionSet()
//                    .HasApiVersion(1.0)
//                    .ReportApiVersions()
//                    .Build();

app.UseMiddleware(typeof(ApiExceptionMiddleware));
#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/V1/swagger.json", "V1.0");
        //options.SwaggerEndpoint($"/swagger/V2/swagger.json", "V2.0");
    });
}

app.UseHttpsRedirection();


#region Endpoints
//TODO: Versao pronta para versionar, porém o Swagger precisa ser implementado corretamente para separar as versoes e mostrar como chamar cada uma delas
//app.MapGet("/GetMessage", () => "This is an example of a minimal API").WithApiVersionSet(versionSet).MapToApiVersion(1.0);
//app.MapGet("/GetMessage", () => "2222222222 This is an example of a minimal API 2").WithApiVersionSet(versionSet).MapToApiVersion(2.0);
//app.MapGet("/GetText", () => "This is yet another example of a minimal API").WithApiVersionSet(versionSet).WithApiVersionSet(versionSet).IsApiVersionNeutral();

// GET /GetMessage?api-version=1.0
// GET /GetMessage

app.MapPost("/car/registerAsync", async (RegisterCarRequest request, IRegisterCarUseCase registerCar) =>
{
    app.Logger.LogInformation($"Novo registro de carro solicitado", request);


    var response = await registerCar.Execute(request.Description, request.Plate);

    return response;

});


app.MapPost("/car/pickupAsync", async (PickupCarRequest request, IPickUpCarUseCase pickupCar) =>
{
    app.Logger.LogInformation($"Novo registro de carro solicitado", request);

    //var validationResult = validator.Validate(request);
    //if (!validationResult.IsValid)
    //    return Results.ValidationProblem(validationResult.ToDictionary());


    var response = await pickupCar.Execute(request.CarId, request.RentedBy, request.Latitude, request.Longitude);
    return response;

});


#endregion


app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}