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
using SensidiaTemplateDotNet.UseCases.PickUpCar;
using FluentValidation;
using Swashbuckle.AspNetCore.Annotations;

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

builder.Services.AddValidatorsFromAssemblyContaining<Program>();


//REPOSITORY
builder.Services.AddScoped<ICarReadOnlyRepository, CarRepository>();
builder.Services.AddScoped<ICarWriteOnlyRepository, CarRepository>();

//SERVICE
builder.Services.AddScoped<IPickUpCarUseCase, PickUpCarUseCase>();
builder.Services.AddScoped<IRegisterCarUseCase, RegisterUseCase>();

builder.Services.AddSingleton<SensidiaTemplateDotNet.Infrastructure.InMemoryDataAcess.Context>();
builder.Services.AddScoped<IMinimalValidator, MinimalValidator>();


builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new HeaderApiVersionReader("Api-Version"),
        new QueryStringApiVersionReader("Api-Version"));
}).EnableApiVersionBinding();




builder.Services.AddMvc(options =>
{
    //options.Filters.Add(typeof(DomainExceptionFilter));
    options.Filters.Add(typeof(ValidateModelAttribute));
});


builder.Services.AddApplicationInsightsTelemetry(options =>
{
    options.ConnectionString = builder.Configuration["ApplicationInsights:ConnectionString"];
});

var app = builder.Build();



app.UseMiddleware(typeof(ErrorHandlingMiddleware));

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

}).WithApiVersionSet(versionSet).MapToApiVersion(1.0);


app.MapPost("/car/pickupAsync", async (PickUpCarRequest request, IPickUpCarUseCase pickupCar, IValidator<PickUpCarRequest> validator) =>
{
    app.Logger.LogInformation($"Novo registro de carro solicitado", request);

    var validationResult = validator.Validate(request);
    if (!validationResult.IsValid)
        return Results.ValidationProblem(validationResult.ToDictionary());


    var response = await pickupCar.Execute(request.CarId, request.RentedBy, request.Latitude, request.Longitude);
    return Results.Ok(response);

}).Produces(200).ProducesValidationProblem(400).WithApiVersionSet(versionSet).MapToApiVersion(1.0);





app.Run();
