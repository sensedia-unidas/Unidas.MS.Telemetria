using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.Interfaces.Services.SubTrip.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Trip.Source;
using Unidas.MS.Telemetria.Application.ViewModels.Driver;
using Unidas.MS.Telemetria.Application.ViewModels.SubTrip;
using Unidas.MS.Telemetria.Application.ViewModels.Trip;

namespace Unidas.MS.Telemetria.Application.Services.Trip.Source
{
    public class MiXTrip : ITripSource
    {

        IClientMiX _client;
        public MiXTrip(IClientMiX client)
        {
            _client = client;

        }
        public async Task<TripResultsVM> Get(string sinceDate, int quantity, long? organizationId = null)
        {

            if (organizationId == null)
                throw new OrganizationIdIsNullException("Trip", SourceEnum.MiX);

            if (String.IsNullOrEmpty(sinceDate))
                throw new SinceDateIsNullException();


            try
            {
                var resultFromMiX = await _client.Trips.GetCreatedSinceForOrganisationAsync(organizationId.Value, sinceDate, quantity, false);



                var vm = new TripResultsVM();
                vm.Result = resultFromMiX;
                vm.OrganizationId = organizationId.Value;
                vm.HasMoreResult = resultFromMiX.HasMoreItems;

                return vm;
            }
            catch (Exception ex)
            {
                return new TripResultsVM()
                {
                    OrganizationId = organizationId.Value
                };
            }



        }
    }
}
