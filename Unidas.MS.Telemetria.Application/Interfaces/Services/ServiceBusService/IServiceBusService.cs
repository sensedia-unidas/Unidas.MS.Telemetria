using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.ViewModels.ServiceBus;

namespace Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBus
{
    public interface IServiceBusService
    {
        Task SendAsync(IList<object> jsons);
        Task SendAsync(object json);
        Task<ServiceBusVM<T>> ReadAsync<T>();
        Task DeleteAsync(Guid guid);

    }
}
