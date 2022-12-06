using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Localization;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Position;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Localization.Source;
using Unidas.MS.Telemetria.Application.Services.Localization.MiX;
using Unidas.MS.Telemetria.Application.Services;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Position.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBus;
using Unidas.MS.Telemetria.Application.Services.Position.Golsat;
using Microsoft.Azure.Amqp.Framing;
using Unidas.MS.Telemetria.Application.ViewModels.Position;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBusService;

namespace Unidas.MS.Telemetria.Application.Commands.Position
{
    public class PositionSaveUseCase : IPositionSaveUseCase
    {


        private IGolsatServiceBusService _golsatServiceBusService;

        public PositionSaveUseCase(IGolsatServiceBusService golsatServiceBusService)
        {
            _golsatServiceBusService = golsatServiceBusService;
        }
        public async Task Execute(int sourceId, GolSatPositionsVM positions)
        {
            IPositionSource source = this.Source(sourceId);

            await source.SaveAsync(positions);

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
