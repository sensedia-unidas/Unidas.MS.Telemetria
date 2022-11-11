using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.ViewModels.SubTrip;
using Unidas.MS.Telemetria.Application.ViewModels.Vehicle;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.Vehicle
{
    public interface IVehicleUseCase
    {
        Task<VehicleVM> Execute( int sourceId, string organizationGroupIds);
    }
}
