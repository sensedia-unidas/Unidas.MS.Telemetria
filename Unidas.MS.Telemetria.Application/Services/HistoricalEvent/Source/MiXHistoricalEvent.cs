using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Services.HistoricalEvent.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.ViewModels.HistoricalEvent;
using Unidas.MS.Telemetria.Domain.Interfaces.Repositories;

namespace Unidas.MS.Telemetria.Application.Services.HistoricalEvent.Source
{
    public class MiXHistoricalEvent : IHistoricalEventSource
    {

        IClientMiX _client;
        IEventFilterReadOnlyRepository _eventFilterReadRepository;
        public MiXHistoricalEvent(IClientMiX client, IEventFilterReadOnlyRepository eventFilterReadRepository)
        {
            _client = client;
            _eventFilterReadRepository = eventFilterReadRepository;

        }
        public async Task<HistoricalEventResultsVM> Get(string sinceDate, int quantity, long? organizationId = null)
        {

            if (organizationId == null)
                throw new OrganizationIdIsNullException("HistoricalEvent", SourceEnum.MiX);

            if (String.IsNullOrEmpty(sinceDate))
                throw new SinceDateIsNullException();

            var eventsFilter = await _eventFilterReadRepository.GetAll();


            var resultFromMiX = await _client.Events.GetCreatedSinceForOrganisationFilteredAsync(organizationId.Value, sinceDate, quantity, eventsFilter.ToList());
            //var resultFromMiX = await _client.Events.GetCreatedSinceForOrganisationAsync(organizationId.Value, sinceDate, quantity );

            
            var vm = new HistoricalEventResultsVM();
            vm.Result = resultFromMiX.Items;
            vm.OrganizationId = organizationId.Value;
            vm.HasMoreResult = resultFromMiX.HasMoreItems;
            
            


            //_client.Events.GetCreatedSinceForOrganisationFiltered

            return vm;

        }
    }
}
