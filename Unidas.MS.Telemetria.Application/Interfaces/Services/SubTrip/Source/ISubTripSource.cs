using Unidas.MS.Telemetria.Application.ViewModels.SubTrip;

namespace Unidas.MS.Telemetria.Application.Interfaces.Services.SubTrip.Source
{
    public interface ISubTripSource
    {
        Task<SubTripResultsVM> Get(string sinceDate, int quantity, long? organizationId = null);
    }
}
