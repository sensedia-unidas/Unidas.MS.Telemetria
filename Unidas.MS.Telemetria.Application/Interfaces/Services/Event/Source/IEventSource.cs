using Unidas.MS.Telemetria.Application.ViewModels.Event;

namespace Unidas.MS.Telemetria.Application.Interfaces.Services.Event.Source
{
    public interface IEventSource
    {
        Task<EventResultsVM> Get(long organizationId);
    }
}
