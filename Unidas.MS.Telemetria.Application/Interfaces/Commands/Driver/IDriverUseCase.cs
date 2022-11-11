using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.ViewModels.Driver;
using Unidas.MS.Telemetria.Application.ViewModels.HistoricalEvent;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.Driver
{
    
    public interface IDriverUseCase
    {
        Task<DriverVM> Execute(int sourceId, string organizationGroupIds);
    }
}
