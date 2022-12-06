using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Queue;
using Unidas.MS.Telemetria.Application.ViewModels.ServiceBus;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.Position
{
    public interface IPositionDeleteUseCase : IBaseQueueDeleteUseCase
    {
        Task Execute(int idSource, Guid guid);
    }
}
