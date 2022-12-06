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
using Unidas.MS.Telemetria.Application.ViewModels.ServiceBus;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBus;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Queue;

namespace Unidas.MS.Telemetria.Application.Commands.Queue
{
    public class BaseQueueReadUseCase : IBaseQueueReadUseCase
    {
        private IServiceBusService _serviceBusService;

        public BaseQueueReadUseCase(IServiceBusService serviceBusService)
        {
            _serviceBusService = serviceBusService;
        }
        public async Task<ServiceBusVM<T>> Execute<T>()
        {

            return await _serviceBusService.ReadAsync<T>();

        }

    }
}
