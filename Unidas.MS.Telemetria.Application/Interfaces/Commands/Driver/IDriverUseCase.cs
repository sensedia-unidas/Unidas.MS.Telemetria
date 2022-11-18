using Unidas.MS.Telemetria.Application.ViewModels.Driver;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.Driver
{

    public interface IDriverUseCase
    {
        Task<DriverVM> Execute(int sourceId, string organizationGroupIds);
    }
}
