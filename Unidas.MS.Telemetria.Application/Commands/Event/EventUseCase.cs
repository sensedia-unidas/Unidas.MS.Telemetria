using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Driver;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Driver.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.Services.Driver.Source;
using Unidas.MS.Telemetria.Application.Services;
using Unidas.MS.Telemetria.Application.ViewModels.Driver;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Event;
using Unidas.MS.Telemetria.Application.ViewModels.Event;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Event.Source;
using Unidas.MS.Telemetria.Application.Services.Event.Source;

namespace Unidas.MS.Telemetria.Application.Commands.Event
{
    public class EventUseCase : IEventUseCase
    {

        private IClientMiX _client;
        public EventUseCase(IClientMiX client)
        {
            _client = client;
        }

        public async Task<EventVM> Execute(int sourceId, string organizationGroupIds)
        {
            List<long> listOrganizationIds = null;
            if (!String.IsNullOrEmpty(organizationGroupIds))
                listOrganizationIds = organizationGroupIds.Split(",").Select(x => long.Parse(x)).ToList();

            var source = this.Source(sourceId);

            var eventResults = new List<EventResultsVM>();

            if (listOrganizationIds == null || listOrganizationIds.Count == 0)
                throw new OrganizationIdIsNullException("Event", (SourceEnum)sourceId);
            else
            {

                List<Task> taskList = new List<Task>();

                listOrganizationIds.ForEach(organziationId =>
                {
                    taskList.Add(Task.Run(async () =>
                    {
                        EventResultsVM eventResult = await source.Get(organziationId);

                        if (eventResult != null)
                            eventResults.Add(eventResult);
                    }));

                });

                Task.WaitAll(taskList.ToArray());

            }

            var eventVM = new EventVM()
            {
                Results = eventResults,
                SourceId = sourceId
            };

            return await Task.FromResult(eventVM);

        }

        private IEventSource Source(int id)
        {
            if (id == (int)SourceEnum.MiX)
                return new MiXEvent(_client);


            throw new SourceIdNotFoundException();
        }
    }


}
