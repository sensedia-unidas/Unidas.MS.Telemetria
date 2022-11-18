using Unidas.MS.Telemetria.Application.ViewModels.Vehicle;

namespace Unidas.MS.Telemetria.Application.Interfaces.Services.Vehicle.Source
{
    public interface IVehicleSource
    {
        Task<VehicleResultsVM> Get(long? organizationId = null);
    }
}
