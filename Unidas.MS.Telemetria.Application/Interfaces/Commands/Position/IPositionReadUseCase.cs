using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Services.Position.Golsat;
using Unidas.MS.Telemetria.Application.ViewModels.Position;
using Unidas.MS.Telemetria.Application.ViewModels.ServiceBus;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.Position
{
    public  interface IPositionReadUseCase
    {
        Task<ServiceBusVM<GolSatItem>> Execute(int idSource);
    }
}
