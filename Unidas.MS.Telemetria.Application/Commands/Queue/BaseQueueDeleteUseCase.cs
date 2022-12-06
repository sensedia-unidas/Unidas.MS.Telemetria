using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Position.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBusService;
using Unidas.MS.Telemetria.Application.Services.Position.Golsat;
using Unidas.MS.Telemetria.Application.Services;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBus;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Queue;

namespace Unidas.MS.Telemetria.Application.Commands.Queue
{
    public class BaseQueueDeleteUseCase : IBaseQueueDeleteUseCase
    { 
        private IServiceBusService _serviceBusService;

        public BaseQueueDeleteUseCase(IServiceBusService serviceBusService)
        {
            _serviceBusService = serviceBusService;
        }
        public async Task Execute( Guid guid)
        {

            await _serviceBusService.DeleteAsync(guid);

        }

    }
}
