using System.ComponentModel.DataAnnotations;

namespace Unidas.MS.Telemetria.Domain.Models.EventFilter
{
    public sealed class EventFilter
    {


        [Key]
        public long Id { get; set; }
        public long Value { get; set; }
        public bool Active { get; set; }

        public static EventFilter Load(long id, long value, bool active)
        {
            EventFilter eventFilter = new EventFilter();
            eventFilter.Id = id;
            eventFilter.Value = value;
            eventFilter.Active = active;

            return eventFilter;

        }
    }
}
