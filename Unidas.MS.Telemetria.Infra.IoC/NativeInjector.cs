﻿using Unidas.MS.Telemetria.Application.Interfaces;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.PickUpCar;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.RegisterCar;
using Unidas.MS.Telemetria.Application.Commands.PickupCar;
using Unidas.MS.Telemetria.Application.Commands.RegisterCar;
using Unidas.MS.Telemetria.Application.Validation;
using Unidas.MS.Telemetria.Domain.Interfaces.Repositories;
using Unidas.MS.Telemetria.Infra.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Unidas.MS.Telemetria.Application.Services.MiX;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Microsoft.EntityFrameworkCore;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.HistoricalEvent;
using Unidas.MS.Telemetria.Application.Commands.HistoricalEvent;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Driver;
using Unidas.MS.Telemetria.Application.Commands.Driver;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Event;
using Unidas.MS.Telemetria.Application.Commands.Event;
using Unidas.MS.Telemetria.Application.Commands.SubTrip;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.SubTrip;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Vehicle;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Trip;
using Unidas.MS.Telemetria.Application.Commands.Trip;
using Unidas.MS.Telemetria.Application.Commands.Vehicle;

namespace Unidas.MS.Telemetria.Infra.IoC
{
    public class NativeInjector
    {
        public static void RegisterServices(IServiceCollection services, string connectionString)
        {
            //REPOSITORY
            services.AddScoped<ICarReadOnlyRepository, CarRepository>();
            services.AddScoped<ICarWriteOnlyRepository, CarRepository>();
            services.AddScoped<IEventFilterReadOnlyRepository, EventFilterRepository>();

            //SERVICE
            services.AddScoped<IPickUpCarUseCase, PickUpCarUseCase>();
            services.AddScoped<IRegisterCarUseCase, RegisterUseCase>();
            services.AddScoped<IClientMiX, ClientMiX>();
            services.AddScoped<IHistoricalEventUseCase, HistoricalEventUseCase>();
            services.AddScoped<IDriverUseCase, DriverUseCase>();
            services.AddScoped<IEventUseCase, EventUseCase>();
            services.AddScoped<ISubTripUseCase, SubTripUseCase>();
            services.AddScoped<IVehicleUseCase, VehicleUseCase>();
            services.AddScoped<ITripUseCase, TripUseCase>();



            services.AddSingleton<InMemoryDbContext>();            
            services.AddSingleton<IMinimalValidator, MinimalValidator>();

            

            services.AddDbContext<TelemetriaContext>(x => x.UseSqlServer(connectionString));

            



        }
    }
}