global using Microsoft.AspNetCore.Mvc;
global using Newtonsoft.Json;
global using System;
global using System.Linq;
global using System.Net;
using FluentValidation;
using Unidas.MS.Telemetria.API.Helpers;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.PickUpCar;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.RegisterCar;

using Unidas.MS.Telemetria.Application.ViewModels.Car.Requests;
using Unidas.MS.Telemetria.Infra.IoC;
using Microsoft.OpenApi.Models;
using Unidas.MS.Telemetria.Application.Services.MiX;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.HistoricalEvent;
using Unidas.MS.Telemetria.Infra;
using Microsoft.Extensions.Hosting;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Driver;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Event;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.SubTrip;
using Unidas.MS.Telemetria.Application.ViewModels.HistoricalEvent;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Vehicle;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Trip;

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


app.MapGet("/historicalEvents/", async (IHistoricalEventUseCase historicalEventCmd, string sinceDate, int sourceId, int quantity, string organizationIds) =>
{
        
    var historicalEvents = await historicalEventCmd.Execute(sinceDate, sourceId, quantity, organizationIds );
   
    return historicalEvents;
});

app.MapGet("/drivers/", async (IDriverUseCase driverCmd, int sourceId,string organizationIds) =>
{

    var drivers = await driverCmd.Execute(sourceId, organizationIds);
    return drivers;
});

app.MapGet("/events/", async (IEventUseCase eventCmd, int sourceId, string organizationIds) =>
{

    var events = await eventCmd.Execute(sourceId, organizationIds);
    return events;
});

app.MapGet("/subTrips/", async (ISubTripUseCase subTripCmd, string sinceDate, int sourceId, int quantity, string organizationIds) =>
{

    var subTrips = await subTripCmd.Execute(sinceDate, sourceId, quantity, organizationIds);
    return subTrips;
});


app.MapGet("/vehicles/", async (IVehicleUseCase vehicleCmd,  int sourceId, string organizationIds) =>
{

    var vehicles = await vehicleCmd.Execute( sourceId,  organizationIds);
    return vehicles;
});


app.MapGet("/trips/", async (ITripUseCase tripCmd, string sinceDate, int sourceId, int quantity, string organizationIds) =>
{

    var trips = await tripCmd.Execute(sinceDate, sourceId, quantity, organizationIds);
    return trips;
});






app.Run();