using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.ViewModels.HistoricalEvent;
using Unidas.MS.Telemetria.Application.ViewModels.SubTrip;

namespace Unidas.MS.Telemetria.Application.Interfaces.Services.SubTrip.Source
{
    public interface ISubTripSource
    {
        Task<SubTripResultsVM> Get(string sinceDate, int quantity, long? organizationId = null);
    }
}
