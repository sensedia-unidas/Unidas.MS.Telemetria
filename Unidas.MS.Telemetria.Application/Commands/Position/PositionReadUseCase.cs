using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Position;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Position.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBus;
using Unidas.MS.Telemetria.Application.Services.Position.Golsat;
using Unidas.MS.Telemetria.Application.Services;
using Unidas.MS.Telemetria.Application.ViewModels.ServiceBus;
using Unidas.MS.Telemetria.Application.ViewModels.Position;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBusService;

namespace Unidas.MS.Telemetria.Application.Commands.Position
{
    public class PositionReadUseCase : IPositionReadUseCase
    {

        private IGolsatServiceBusService _golsatServiceBusService;

        public PositionReadUseCase(IGolsatServiceBusService golsatServiceBusService)
        {
            _golsatServiceBusService = golsatServiceBusService;
        }
        public async Task<ServiceBusVM<GolSatItem>> Execute(int sourceId)
        {
            IPositionSource source = this.Source(sourceId);

            return await source.ReadAsync();

        }

        private IPositionSource Source(int id)
        {
            if (id == (int)SourceEnum.Golsat)
            {
                return new GolsatPosition(_golsatServiceBusService);

            }
            throw new SourceIdNotFoundException();
        }
    }
}
