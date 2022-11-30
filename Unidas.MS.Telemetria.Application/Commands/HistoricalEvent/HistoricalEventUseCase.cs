
using Microsoft.Extensions.DependencyInjection;
using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.HistoricalEvent;
using Unidas.MS.Telemetria.Application.Interfaces.Services.HistoricalEvent.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.Services;
using Unidas.MS.Telemetria.Application.Services.HistoricalEvent.MiX;
using Unidas.MS.Telemetria.Application.ViewModels.HistoricalEvent;
using Unidas.MS.Telemetria.Domain.Interfaces.Repositories;

namespace Unidas.MS.Telemetria.Application.Commands.HistoricalEvent
{
    public class HistoricalEventUseCase : IHistoricalEventUseCase
    {
        private IClientMiX _client;

        IEventFilterReadOnlyRepository _eventFilterReadRepository;
        IServiceScopeFactory _serviceScopeFactory;
        public HistoricalEventUseCase(IClientMiX client, IServiceScopeFactory serviceScopeFactory)
        {

            _client = client;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task<HistoricalEventVM> Execute(string sinceDate, int sourceId, int quantity, string organizationIds)
        {

            List<long> listOrganizationIds = null;
            if (!String.IsNullOrEmpty(organizationIds))
                listOrganizationIds = organizationIds.Split(",").Select(x => long.Parse(x)).ToList();

            var source = this.Source(sourceId);

            var historicalEventsOrganization = new List<HistoricalEventResultsVM>();

            if (listOrganizationIds == null || listOrganizationIds.Count == 0)
                historicalEventsOrganization.Add(await source.Get(sinceDate, quantity));
            else
            {


                List<Task> taskList = new List<Task>();

                listOrganizationIds.ForEach(organziationId =>
                {
                    taskList.Add(Task.Run(async () =>
                    {
                        HistoricalEventResultsVM eventResult = await source.Get(sinceDate, quantity, organziationId);

                        if (eventResult != null)
                            historicalEventsOrganization.Add(eventResult);
                    }));

                });

                Task.WaitAll(taskList.ToArray());

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
                return new MiXHistoricalEvent(_client, _serviceScopeFactory);


            throw new SourceIdNotFoundException();
        }
    }
}
