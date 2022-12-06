using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Interfaces.Services.ServiceBusService;

namespace Unidas.MS.Telemetria.Application.Services.ServiceBus
{
    public  class GolsatServiceBusService : ServiceBusService , IGolsatServiceBusService
    {
        public GolsatServiceBusService (): base(Config.GetFromAppSettings("ServiceBus:Golsat:ConnectionString1"), Config.GetFromAppSettings("ServiceBus:Golsat:QueueName"))
        {

        }
    }
}
