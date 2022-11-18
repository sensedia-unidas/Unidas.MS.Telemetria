using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Vehicle.Source;
using Unidas.MS.Telemetria.Application.ViewModels.Vehicle;

namespace Unidas.MS.Telemetria.Application.Services.Vehicle.Source
{
    public class MiXVehicle : IVehicleSource
    {

        IClientMiX _client;
        public MiXVehicle(IClientMiX client)
        {
            _client = client;

        }
        public async Task<VehicleResultsVM> Get(long? organizationId = null)
        {

            if (organizationId == null)
                throw new OrganizationIdIsNullException("Vehicle", SourceEnum.MiX);



            var resultFromMiX = await _client.Assets.GetAllAsync(organizationId.Value);



            var vm = new VehicleResultsVM();
            vm.Result = resultFromMiX;
            vm.OrganizationId = organizationId.Value;



            return vm;

        }
    }
}
