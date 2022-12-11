using Unidas.MS.Telemetria.Application.Interfaces.Services.Driver.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.ViewModels.Driver;

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




            return vm;
        }
    }
}
