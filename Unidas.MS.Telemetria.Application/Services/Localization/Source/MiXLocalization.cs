using MiX.Integrate.API.Client;
using MiX.Integrate.Shared.Entities.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Localization.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.ViewModels.Localization;

namespace Unidas.MS.Telemetria.Application.Services.Localization.Source
{
    public class MiXLocalization : ILocalizationSource
    {
        DateTime lastDate = DateTime.MinValue;
        Dictionary<long, List<Location>> dicLocations = new Dictionary<long, List<Location>>();
        private readonly IClientMiX _clientMiX;

        public MiXLocalization(IClientMiX clientMiX)
        {
            _clientMiX = clientMiX;
        }
        private async Task<List<Location>> GetLocations(long organizationId)
        {
            if (lastDate.Date < DateTime.UtcNow.Date)
            {
                dicLocations = new Dictionary<long, List<Location>>();
                lastDate = DateTime.UtcNow;
            }

            if (!dicLocations.ContainsKey(organizationId))
            {
                List<Location> locations = await _clientMiX.Locations.GetAllAsync(organizationId, true, false);
                dicLocations.Add(organizationId, locations);
            }

            var currentLocations = dicLocations[organizationId];

            currentLocations = currentLocations.Where(p => p.Name.ToUpperInvariant().Contains("PÁTIO")
                                                            || p.Name.ToUpperInvariant().Contains("MANUTENÇÃO")).ToList();
            return currentLocations;
                

        }



        public async Task<LocalizationResultsVM> GetAsync(string sinceDate, int quantity, long? organizationId = null)
        {
            if (organizationId == null)
                throw new OrganizationIdIsNullException("Locations", SourceEnum.MiX);

            if (String.IsNullOrEmpty(sinceDate))
                throw new SinceDateIsNullException();

            var locations = await GetLocations(organizationId.Value);

            var positions = await _clientMiX.Positions.GetCreatedSinceForOrganisationAsync(organizationId.Value, sinceDate, quantity);

            var vm = new LocalizationResultsVM();
            vm.OrganizationId = organizationId.Value;
            vm.HasMoreResult = positions.HasMoreItems;
            vm.ResultPositions = positions.Items;
            vm.ResultLocations = locations;

            return await Task.FromResult(vm);
        }
    }
}
