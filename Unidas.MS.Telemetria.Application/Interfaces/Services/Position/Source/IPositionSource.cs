using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.ViewModels.Event;
using Unidas.MS.Telemetria.Application.ViewModels.ServiceBus;

namespace Unidas.MS.Telemetria.Application.Interfaces.Services.Position.Source
{
    public interface IPositionSource
    {
        Task SaveAsync(object json);
        Task<ServiceBusVM<object>> ReadAsync();
        Task DeleteAsync(Guid guid);
    }
}
