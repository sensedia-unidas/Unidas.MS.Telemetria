#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Unidas.MS.Telemetria.API/Unidas.MS.Telemetria.API.csproj", "Unidas.MS.Telemetria.API/"]
COPY ["Unidas.MS.Telemetria.Application/Unidas.MS.Telemetria.Application.csproj", "Unidas.MS.Telemetria.Application/"]
COPY ["Unidas.MS.Telemetria.Domain/Unidas.MS.Telemetria.Domain.csproj", "Unidas.MS.Telemetria.Domain/"]
COPY ["Unidas.MS.Telemetria.Infra.IoC/Unidas.MS.Telemetria.Infra.IoC.csproj", "Unidas.MS.Telemetria.Infra.IoC/"]
COPY ["Unidas.MS.Telemetria.Infra/Unidas.MS.Telemetria.Infra.csproj", "Unidas.MS.Telemetria.Infra/"]
RUN dotnet restore "Unidas.MS.Telemetria.API/Unidas.MS.Telemetria.API.csproj"
COPY . .
WORKDIR "/src/Unidas.MS.Telemetria.API"
RUN dotnet build "Unidas.MS.Telemetria.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Unidas.MS.Telemetria.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Unidas.MS.Telemetria.API.dll"]