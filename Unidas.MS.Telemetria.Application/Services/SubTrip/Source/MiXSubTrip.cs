using MiX.Integrate.Shared.Entities.Trips;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Services.HistoricalEvent.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.Interfaces.Services.SubTrip.Source;
using Unidas.MS.Telemetria.Application.ViewModels.HistoricalEvent;
using Unidas.MS.Telemetria.Application.ViewModels.SubTrip;
using Unidas.MS.Telemetria.Domain.Interfaces.Repositories;

namespace Unidas.MS.Telemetria.Application.Services.SubTrip.Source
{
    public class MiXSubTrip : ISubTripSource
    {

        IClientMiX _client;
        public MiXSubTrip(IClientMiX client)
        {
            _client = client;

        }
        public async Task<SubTripResultsVM> Get(string sinceDate, int quantity, long? organizationId = null)
        {

            if (organizationId == null)
                throw new OrganizationIdIsNullException("SubTrip", SourceEnum.MiX);

            if (String.IsNullOrEmpty(sinceDate))
                throw new SinceDateIsNullException();



            var resultFromMiX = await _client.Trips.GetCreatedSinceForOrganisationAsync(organizationId.Value, sinceDate, quantity, true);

            List<object> subTrips = new();

            foreach (var trip in resultFromMiX.Items)
                subTrips.AddRange(trip.SubTrips);
            

            var vm = new SubTripResultsVM();
            vm.Result = subTrips;
            vm.OrganizationId = organizationId.Value;
            vm.HasMoreResult = resultFromMiX.HasMoreItems;





            return vm;

        }
    }
}
