using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidas.MS.Telemetria.Domain.Models.ServiceBus.Position
{
    public class PositionMessage
    {
        public Guid Id { get; set; }
        public int SourceId { get; set; }
        public DateTime Date { get; set; }
        public object Json { get; set; }
    }
}
