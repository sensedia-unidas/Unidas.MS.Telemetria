using Azure.Messaging.ServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidas.MS.Telemetria.Application.ViewModels.ServiceBus
{
    public class ServiceBusVM<T>
    {
        public Guid Guid { get; set; }
        public List<T> Messages { get; set; }
    }
}
