global using Microsoft.AspNetCore.Mvc;
global using Newtonsoft.Json;
global using System;
global using System.Linq;
global using System.Net;
using Microsoft.OpenApi.Models;
using Unidas.MS.Telemetria.API.Helpers;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Driver;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Event;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.HistoricalEvent;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Localization;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Position;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.SubTrip;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Trip;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Vehicle;
using Unidas.MS.Telemetria.Application.ViewModels.Driver;
using Unidas.MS.Telemetria.Infra.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("V1", new OpenApiInfo() { Title = "API V1", Version = "V1.0" });
    options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    options.CustomSchemaIds(x => x.FullName);
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

NativeInjector.RegisterServices(builder.Services, connectionString);



builder.Services.AddMvc(options =>
{
    options.Filters.Add(typeof(ValidateActionFilterAttribute));
});


var app = builder.Build();


app.UseMiddleware(typeof(ApiExceptionMiddleware));


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint($"/swagger/V1/swagger.json", "V1.0");
    });
}

app.UseHttpsRedirection();


//app.Logger.LogInformation($"Novo registro de carro solicitado", request);


app.MapGet("/mix/historicalEvents/", async (IHistoricalEventUseCase historicalEventCmd, string sinceDate, int quantity, string organizationIds) =>
{

    var historicalEvents = await historicalEventCmd.Execute(sinceDate, 0, quantity, organizationIds);

    return historicalEvents;
});

app.MapGet("/mix/drivers/", async (IDriverUseCase driverCmd, string organizationIds) =>
{

    var drivers = await driverCmd.Execute(0, organizationIds);
    return drivers;
});

app.MapGet("/mix/events/", async (IEventUseCase eventCmd, string organizationIds) =>
{

    var events = await eventCmd.Execute(0, organizationIds);
    return events;
});

app.MapGet("/mix/subTrips/", async (ISubTripUseCase subTripCmd, string sinceDate, int quantity, string organizationIds) =>
{

    var subTrips = await subTripCmd.Execute(sinceDate, 0, quantity, organizationIds);
    return subTrips;
});


app.MapGet("/mix/vehicles/", async (IVehicleUseCase vehicleCmd, string organizationIds) =>
{

    var vehicles = await vehicleCmd.Execute(0, organizationIds);
    return vehicles;
});


app.MapGet("/mix/trips/", async (ITripUseCase tripCmd, string sinceDate, int quantity, string organizationIds) =>
{

    var trips = await tripCmd.Execute(sinceDate, 0, quantity, organizationIds);
    return trips;
});

app.MapGet("/mix/locations/", async (ILocalizationUseCase localizationCmd, string sinceDate, int quantity, string organizationIds) =>
{

    var locations = await localizationCmd.Execute(sinceDate, 0, quantity, organizationIds);

    return locations;
});

app.MapPost("/golsat/positions/save", async (IPositionSaveUseCase positionSaveCmd , object obj) =>
{

    await positionSaveCmd.Execute(1, obj);

    return Results.Ok();
});

app.MapGet("/golsat/positions/", async (IPositionReadUseCase positionCmd) =>
{

    return await positionCmd.Execute(1);

});

app.MapDelete("/golsat/positions/remove", async (IPositionDeleteUseCase positionCmd, Guid guid) =>
{

    await positionCmd.Execute(1, guid);

    return Results.Ok();
});





app.Run();