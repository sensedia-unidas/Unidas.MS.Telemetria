using Unidas.MS.Telemetria.Application.ViewModels.SubTrip;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.SubTrip
{
    public interface ISubTripUseCase
    {
        Task<SubTripVM> Execute(string sinceDate, int sourceId, int quantity, string organizationGroupIds);
    }
}
