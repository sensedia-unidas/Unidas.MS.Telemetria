using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.ViewModels.Driver;
using Unidas.MS.Telemetria.Application.ViewModels.Event;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.Event
{
    public interface IEventUseCase
    {
        Task<EventVM> Execute(int sourceId, string organizationGroupIds);
    }
}
