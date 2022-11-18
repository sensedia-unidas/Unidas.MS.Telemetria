using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.SubTrip;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.Interfaces.Services.SubTrip.Source;
using Unidas.MS.Telemetria.Application.Services;
using Unidas.MS.Telemetria.Application.Services.SubTrip.Source;
using Unidas.MS.Telemetria.Application.ViewModels.SubTrip;

namespace Unidas.MS.Telemetria.Application.Commands.SubTrip
{
    public class SubTripUseCase : ISubTripUseCase
    {
        private IClientMiX _client;
        public SubTripUseCase(IClientMiX client)
        {
            _client = client;
        }

        public async Task<SubTripVM> Execute(string sinceDate, int sourceId, int quantity, string organizationIds)
        {

            List<long> listOrganizationIds = null;
            if (!String.IsNullOrEmpty(organizationIds))
                listOrganizationIds = organizationIds.Split(",").Select(x => long.Parse(x)).ToList();

            var source = this.Source(sourceId);

            var subTrips = new List<SubTripResultsVM>();

            if (listOrganizationIds == null || listOrganizationIds.Count == 0)
                subTrips.Add(await source.Get(sinceDate, quantity));
            else
            {


                List<Task> taskList = new List<Task>();

                listOrganizationIds.ForEach(organziationId =>
                {
                    taskList.Add(Task.Run(async () =>
                    {
                        SubTripResultsVM subTripResult = await source.Get(sinceDate, quantity, organziationId);

                        if (subTripResult != null)
                            subTrips.Add(subTripResult);
                    }));

                });

                Task.WaitAll(taskList.ToArray());

            }

            var subTrip = new SubTripVM()
            {
                Results = subTrips,
                SourceId = sourceId
            };

            return await Task.FromResult(subTrip);

        }

        private ISubTripSource Source(int id)
        {
            if (id == (int)SourceEnum.MiX)
                return new MiXSubTrip(_client);


            throw new SourceIdNotFoundException();
        }
    }
}
