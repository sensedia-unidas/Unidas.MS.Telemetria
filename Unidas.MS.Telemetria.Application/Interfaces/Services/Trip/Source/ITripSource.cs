using Unidas.MS.Telemetria.Application.ViewModels.Trip;

namespace Unidas.MS.Telemetria.Application.Interfaces.Services.Trip.Source
{
    public interface ITripSource
    {
        Task<TripResultsVM> Get(string sinceDate, int quantity, long? organizationId = null);
    }
}
