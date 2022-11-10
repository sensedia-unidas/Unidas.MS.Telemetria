using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Services.HistoricalEvent.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.ViewModels.Historical;

namespace Unidas.MS.Telemetria.Application.Services.HistoricalEvent.Source
{
    public class MiXHistoricalEvent : IHistoricalEventSource
    {

        IClientMiX _client;
        public MiXHistoricalEvent(IClientMiX client)
        {
            _client = client;

        }
        public Task<HistoricalEventResultsVM> Get(string sinceDate, int quantity, string? organizationId = null)
        {

            if (String.IsNullOrEmpty(organizationId))
                throw new OrganizationIdIsNullException("HistoricalEvent", SourceEnum.MiX);

            if (String.IsNullOrEmpty(sinceDate))
                throw new SinceDateIsNullException();



            //_client.Events.GetCreatedSinceForOrganisationFiltered

            return null;

        }
    }
}
