using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.ViewModels.SubTrip;
using Unidas.MS.Telemetria.Application.ViewModels.Trip;

namespace Unidas.MS.Telemetria.Application.Interfaces.Services.Trip.Source
{
    public interface ITripSource
    {
        Task<TripResultsVM> Get(string sinceDate, int quantity, long? organizationId = null);
    }
}
