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
using Unidas.MS.Telemetria.Application.ViewModels.Position;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBus;
using static IdentityModel.OidcConstants;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Queue;

namespace Unidas.MS.Telemetria.Application.Commands.Queue
{
    public class BaseQueueSaveUseCase : IBaseQueueSaveUseCase
    {

        private IServiceBusService _serviceBusService;

        public BaseQueueSaveUseCase(IServiceBusService serviceBusService)
        {
            _serviceBusService = serviceBusService;
        }
        public async Task Execute<T>(int sourceId, T events)
        {
            await _serviceBusService.SendAsync(events);
        }

    }
}
