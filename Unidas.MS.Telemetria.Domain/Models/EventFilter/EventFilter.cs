using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Domain.Models.Cars;

namespace Unidas.MS.Telemetria.Domain.Models.EventFilter
{
    public sealed class EventFilter 
    {
       

        [Key]
        public long Id { get; set; }
        public bool Active { get; set; }

        public static EventFilter Load(long id, bool active)
        {
            EventFilter eventFilter = new EventFilter();
            eventFilter.Id = id;
            eventFilter.Active = active;

            return eventFilter;

        }
    }
}
