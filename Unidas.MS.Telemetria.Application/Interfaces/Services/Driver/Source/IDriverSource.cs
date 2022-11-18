using Unidas.MS.Telemetria.Application.ViewModels.Driver;

namespace Unidas.MS.Telemetria.Application.Interfaces.Services.Driver.Source
{
    public interface IDriverSource
    {
        Task<DriverResultsVM> Get(long organizationId);
    }
}
