using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Position.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBus;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBusService;
using Unidas.MS.Telemetria.Application.ViewModels.Position;
using Unidas.MS.Telemetria.Application.ViewModels.ServiceBus;

namespace Unidas.MS.Telemetria.Application.Services.Position.Golsat
{
    public class GolsatPosition : IPositionSource
    {

        private IGolsatServiceBusService _serviceBusService;
        public GolsatPosition(IGolsatServiceBusService serviceBusService)
        {
            _serviceBusService = serviceBusService;
        }


        public async Task SaveAsync(GolSatPositionsVM positions)
        {
            await _serviceBusService.SendAsync<GolSatItem>(positions.positions);
        }

        public async Task<ServiceBusVM<GolSatItem>> ReadAsync()
        {
            ServiceBusVM<GolSatItem> messageVM = await _serviceBusService.ReadAsync<GolSatItem>();

            return messageVM;
        }

        public async Task DeleteAsync(Guid guid)
        {
            await _serviceBusService.DeleteAsync(guid);
        }
    }
}
