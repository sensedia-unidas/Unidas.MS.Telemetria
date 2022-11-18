using Unidas.MS.Telemetria.Application.ViewModels.Trip;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.Trip
{
    public interface ITripUseCase
    {
        Task<TripVM> Execute(string sinceDate, int sourceId, int quantity, string organizationGroupIds);
    }
}
