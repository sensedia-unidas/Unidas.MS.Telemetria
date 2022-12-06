using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Queue.Ituran;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBus;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBusService;

namespace Unidas.MS.Telemetria.Application.Commands.Queue.Ituran
{
    public class IturanQueueSaveUseCase : BaseQueueSaveUseCase, IIturanQueueSaveUseCase
    {
        public IturanQueueSaveUseCase(IIturanServiceBusService serviceBusService) : base(serviceBusService)
        {
        }
    }
}
