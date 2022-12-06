using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.ViewModels.Position;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.Queue
{
    public interface IBaseQueueSaveUseCase
    {
        Task Execute<T>(int idSource, T events);
    }
}
