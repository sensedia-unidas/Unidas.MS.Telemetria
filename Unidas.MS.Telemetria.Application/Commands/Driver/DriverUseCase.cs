using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Driver;
using Unidas.MS.Telemetria.Application.Interfaces.Services.HistoricalEvent.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.Services.HistoricalEvent.Source;
using Unidas.MS.Telemetria.Application.Services;
using Unidas.MS.Telemetria.Application.ViewModels.Driver;
using Unidas.MS.Telemetria.Domain.Interfaces.Repositories;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Driver.Source;
using Unidas.MS.Telemetria.Application.ViewModels.HistoricalEvent;

namespace Unidas.MS.Telemetria.Application.Commands.Driver
{
    public class DriverUseCase : IDriverUseCase
    {

        private IClientMiX _client;
        public DriverUseCase(IClientMiX client)
        {
            _client = client;
        }

        public async Task<DriverVM> Execute(int sourceId, string organizationGroupIds)
        {
            List<long> listOrganizationIds = null;
            if (!String.IsNullOrEmpty(organizationGroupIds))
                listOrganizationIds = organizationGroupIds.Split(",").Select(x => long.Parse(x)).ToList();

            var source = this.Source(sourceId);

            var driverResults = new List<DriverResultsVM>();

            if (listOrganizationIds == null || listOrganizationIds.Count == 0)
                throw new OrganizationIdIsNullException("Driver", (SourceEnum)sourceId);
            else
            {

                List<Task> taskList = new List<Task>();

                listOrganizationIds.ForEach(organziationId =>
                {
                    taskList.Add(Task.Run(async () =>
                    {
                        DriverResultsVM driverResult = await source.Get(organziationId);

                        if (driverResult != null)
                            driverResults.Add(driverResult);
                    }));

                });

                Task.WaitAll(taskList.ToArray());

            }

            var driverVM = new DriverVM()
            {
                Results = driverResults,
                SourceId = sourceId
            };

            return await Task.FromResult(driverVM);

        }

        private IDriverSource Source(int id)
        {
            if (id == (int)SourceEnum.MiX)
                return new MiXDriver(_client);


            throw new SourceIdNotFoundException();
        }
    }


}
