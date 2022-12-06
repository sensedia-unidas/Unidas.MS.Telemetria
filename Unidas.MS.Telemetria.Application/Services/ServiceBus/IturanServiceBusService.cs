using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBusService;

namespace Unidas.MS.Telemetria.Application.Services.ServiceBus
{
    public class IturanServiceBusService : ServiceBusService, IIturanServiceBusService
    {
        public IturanServiceBusService() : base(Config.GetFromAppSettings("ServiceBus:Ituran:ConnectionString1"), Config.GetFromAppSettings("ServiceBus:Ituran:QueueName"))
        {

        }
    }
}