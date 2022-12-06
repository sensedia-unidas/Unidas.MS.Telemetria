using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.Queue
{
    public interface IBaseQueueDeleteUseCase
    {
        Task Execute( Guid guid);
    }
}
