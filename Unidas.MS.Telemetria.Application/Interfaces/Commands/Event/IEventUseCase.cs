using Unidas.MS.Telemetria.Application.ViewModels.Event;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.Event
{
    public interface IEventUseCase
    {
        Task<EventVM> Execute(int sourceId, string organizationGroupIds);
    }
}
