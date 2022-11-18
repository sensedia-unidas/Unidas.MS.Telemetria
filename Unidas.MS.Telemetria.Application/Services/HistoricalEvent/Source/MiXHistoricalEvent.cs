using Microsoft.Extensions.DependencyInjection;
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
        IServiceScopeFactory _serviceScopeFactory;

        public MiXHistoricalEvent(IClientMiX client, IServiceScopeFactory serviceScopeFactory)
        {
            _client = client;
            _serviceScopeFactory = serviceScopeFactory;

        }
        public async Task<HistoricalEventResultsVM> Get(string sinceDate, int quantity, long? organizationId = null)
        {

            if (organizationId == null)
                throw new OrganizationIdIsNullException("HistoricalEvent", SourceEnum.MiX);

            if (String.IsNullOrEmpty(sinceDate))
                throw new SinceDateIsNullException();

            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var eventFilterReadRepository = scope.ServiceProvider.GetService<IEventFilterReadOnlyRepository>();

                IEnumerable<long> eventsFilter = await eventFilterReadRepository.GetAll();


                var resultFromMiX = await _client.Events.GetCreatedSinceForOrganisationFilteredAsync(organizationId.Value, sinceDate, quantity, eventsFilter.ToList());
                //var resultFromMiX = await _client.Events.GetCreatedSinceForOrganisationAsync(organizationId.Value, sinceDate, quantity );


                var vm = new HistoricalEventResultsVM();
                vm.Result = resultFromMiX.Items;
                vm.OrganizationId = organizationId.Value;
                vm.HasMoreResult = resultFromMiX.HasMoreItems;





                return vm;
            }

           

        }
    }
}
