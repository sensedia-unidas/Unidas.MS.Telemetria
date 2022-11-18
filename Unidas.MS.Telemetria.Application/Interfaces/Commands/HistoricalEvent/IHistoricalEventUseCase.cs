using Unidas.MS.Telemetria.Application.ViewModels.HistoricalEvent;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.HistoricalEvent
{
    public interface IHistoricalEventUseCase
    {
        Task<HistoricalEventVM> Execute(string sinceDate, int sourceId, int quantity, string organizationGroupIds);
    }
}
