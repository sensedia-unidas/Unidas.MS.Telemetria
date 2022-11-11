using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Driver.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.ViewModels.Driver;
using Unidas.MS.Telemetria.Application.ViewModels.HistoricalEvent;

namespace Unidas.MS.Telemetria.Application.Services.Driver.Source
{
    public class MiXDriver : IDriverSource
    {
        IClientMiX _client;
        public MiXDriver(IClientMiX client)
        {
            _client = client;

        }

        public async Task<DriverResultsVM> Get(long organizationId)
        {
            var resultFromMiX = await _client.Drivers.GetAllDriversAsync(organizationId);


            var vm = new DriverResultsVM();
            vm.Result = resultFromMiX;
            vm.OrganizationId = organizationId;




            //_client.Events.GetCreatedSinceForOrganisationFiltered

            return vm;
        }
    }
}
