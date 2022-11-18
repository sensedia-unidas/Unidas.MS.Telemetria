using Unidas.MS.Telemetria.Application.ViewModels.Vehicle;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.Vehicle
{
    public interface IVehicleUseCase
    {
        Task<VehicleVM> Execute(int sourceId, string organizationGroupIds);
    }
}
