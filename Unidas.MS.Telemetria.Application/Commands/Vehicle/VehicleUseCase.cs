using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Event;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Event.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.Services.Event.Source;
using Unidas.MS.Telemetria.Application.Services;
using Unidas.MS.Telemetria.Application.ViewModels.Event;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Vehicle;
using Unidas.MS.Telemetria.Application.ViewModels.Vehicle;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Vehicle.Source;

namespace Unidas.MS.Telemetria.Application.Commands.Vehicle
{
    public class VehicleUseCase : IVehicleUseCase
    {

        private IClientMiX _client;
        public VehicleUseCase(IClientMiX client)
        {
            _client = client;
        }

        public async Task<VehicleVM> Execute(int sourceId, string organizationGroupIds)
        {
            List<long> listOrganizationIds = null;
            if (!String.IsNullOrEmpty(organizationGroupIds))
                listOrganizationIds = organizationGroupIds.Split(",").Select(x => long.Parse(x)).ToList();

            var source = this.Source(sourceId);

            var vehicleResults = new List<VehicleResultsVM>();

            if (listOrganizationIds == null || listOrganizationIds.Count == 0)
                throw new OrganizationIdIsNullException("Event", (SourceEnum)sourceId);
            else
            {

                List<Task> taskList = new List<Task>();

                listOrganizationIds.ForEach(organziationId =>
                {
                    taskList.Add(Task.Run(async () =>
                    {
                        VehicleResultsVM vehicleResult = await source.Get(organziationId);

                        if (vehicleResult != null)
                            vehicleResults.Add(vehicleResult);
                    }));

                });

                Task.WaitAll(taskList.ToArray());

            }

            var vehicleVM = new VehicleVM()
            {
                Results = vehicleResults,
                SourceId = sourceId
            };

            return await Task.FromResult(vehicleVM);

        }

        private IVehicleSource Source(int id)
        {
            if (id == (int)SourceEnum.MiX)
                return new MiXVehicle(_client);


            throw new SourceIdNotFoundException();
        }
    }


}
