using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Trip;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Trip.Source;
using Unidas.MS.Telemetria.Application.Services;
using Unidas.MS.Telemetria.Application.Services.Trip.MiX;
using Unidas.MS.Telemetria.Application.ViewModels.Trip;

namespace Unidas.MS.Telemetria.Application.Commands.Trip
{
    public class TripUseCase : ITripUseCase
    {
        private IClientMiX _client;
        public TripUseCase(IClientMiX client)
        {
            _client = client;
        }

        public async Task<TripVM> Execute(string sinceDate, int sourceId, int quantity, string organizationIds)
        {

            List<long> listOrganizationIds = null;
            if (!String.IsNullOrEmpty(organizationIds))
                listOrganizationIds = organizationIds.Split(",").Select(x => long.Parse(x)).ToList();

            var source = this.Source(sourceId);

            var trips = new List<TripResultsVM>();

            if (listOrganizationIds == null || listOrganizationIds.Count == 0)
                trips.Add(await source.Get(sinceDate, quantity));
            else
            {


                List<Task> taskList = new List<Task>();

                listOrganizationIds.ForEach(organziationId =>
                {
                    taskList.Add(Task.Run(async () =>
                    {
                        TripResultsVM tripResult = await source.Get(sinceDate, quantity, organziationId);

                        if (tripResult != null)
                            trips.Add(tripResult);
                    }));

                });

                Task.WaitAll(taskList.ToArray());

            }

            var trip = new TripVM()
            {
                Results = trips,
                SourceId = sourceId
            };

            return await Task.FromResult(trip);

        }

        private ITripSource Source(int id)
        {
            if (id == (int)SourceEnum.MiX)
                return new MiXTrip(_client);


            throw new SourceIdNotFoundException();
        }
    }
}
