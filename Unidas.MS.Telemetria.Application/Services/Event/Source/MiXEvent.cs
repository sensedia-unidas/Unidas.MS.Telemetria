using Unidas.MS.Telemetria.Application.Interfaces.Services.Event.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.ViewModels.Event;

namespace Unidas.MS.Telemetria.Application.Services.Event.Source
{
    public class MiXEvent : IEventSource
    {
        IClientMiX _client;
        public MiXEvent(IClientMiX client)
        {
            _client = client;

        }

        public async Task<EventResultsVM> Get(long organizationId)
        {
            var resultFromMiX = await _client.LibraryEvents.GetAllLibraryEventsAsync(organizationId);


            var vm = new EventResultsVM();
            vm.Result = resultFromMiX;
            vm.OrganizationId = organizationId;




            return vm;
        }
    }
}
