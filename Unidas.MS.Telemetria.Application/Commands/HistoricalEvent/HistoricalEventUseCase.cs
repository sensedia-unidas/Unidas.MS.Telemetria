
using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.HistoricalEvent;
using Unidas.MS.Telemetria.Application.Interfaces.Services.HistoricalEvent.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.Services;
using Unidas.MS.Telemetria.Application.Services.HistoricalEvent.Source;
using Unidas.MS.Telemetria.Application.ViewModels.Historical;
using Unidas.MS.Telemetria.Domain.Interfaces.Repositories;

namespace Unidas.MS.Telemetria.Application.Commands.HistoricalEvent
{
    public class HistoricalEventUseCase : IHistoricalEventUseCase
    {
        private IClientMiX _client;
        IEventFilterReadOnlyRepository _eventFilterReadRepository;
        public HistoricalEventUseCase(IClientMiX client, IEventFilterReadOnlyRepository eventFilterReadRepository)
        {
            _client = client;
            _eventFilterReadRepository = eventFilterReadRepository;
        }

        public async Task<HistoricalEventVM> Execute(string sinceDate, int sourceId, int quantity, List<string>? organizationGroupIds = null)
        {
            var source = this.Source(sourceId);

            var historicalEventsOrganization = new List<HistoricalEventResultsVM>();

            if (organizationGroupIds == null || organizationGroupIds.Count == 0)
                historicalEventsOrganization.Add(await source.Get(sinceDate,quantity));
            else
            {

                organizationGroupIds.ForEach(async organziationId =>
                {
                    HistoricalEventResultsVM eventResult = await source.Get(sinceDate, quantity, organziationId);
                    historicalEventsOrganization.Add(eventResult);
                });
                
            }

            var historicalEvent = new HistoricalEventVM()
            {
                Results = historicalEventsOrganization,
                SourceId = sourceId
            };

            return await Task.FromResult(historicalEvent);

        }

        private IHistoricalEventSource Source(int id)
        {
            if (id == (int)SourceEnum.MiX)
                return new MiXHistoricalEvent(_client);


            throw new SourceIdNotFoundException();
        }
    }
}
