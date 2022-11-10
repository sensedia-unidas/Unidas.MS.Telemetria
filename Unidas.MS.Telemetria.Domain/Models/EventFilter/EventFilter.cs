using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Domain.Models.Cars;

namespace Unidas.MS.Telemetria.Domain.Models.EventFilter
{
    public sealed class EventFilter : IEntity, IAggregateRoot
    {
        public Guid Id { get; private set; }

        public long Value { get; set; }
        public bool Active { get; set; }

        public static EventFilter Load(Guid id, long value, bool active)
        {
            EventFilter eventFilter = new EventFilter();
            eventFilter.Id = id;
            eventFilter.Value = value;
            eventFilter.Active = active;

            return eventFilter;

        }
    }
}
