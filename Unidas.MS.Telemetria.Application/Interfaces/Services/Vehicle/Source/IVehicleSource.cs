using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.ViewModels.SubTrip;
using Unidas.MS.Telemetria.Application.ViewModels.Vehicle;

namespace Unidas.MS.Telemetria.Application.Interfaces.Services.Vehicle.Source
{
    public  interface IVehicleSource
    {
        Task<VehicleResultsVM> Get(long? organizationId = null);
    }
}
