using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Position.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBus;
using Unidas.MS.Telemetria.Application.ViewModels.ServiceBus;

namespace Unidas.MS.Telemetria.Application.Services.Position.Golsat
{
    public class GolsatPosition : IPositionSource
    {

        private IServiceBusService _serviceBusService;
        public GolsatPosition(IServiceBusService serviceBusService)
        {
            _serviceBusService = serviceBusService;
        }

        
        public async Task SaveAsync(object json)
        {
            await _serviceBusService.SendAsync(json);
        }

        public async Task<ServiceBusVM<object>> ReadAsync()
        {
            ServiceBusVM<object> messageVM = await _serviceBusService.ReadAsync<object>();

            return messageVM;
        }

        public async Task DeleteAsync(Guid guid)
        {
            await _serviceBusService.DeleteAsync(guid);
        }
    }
}
