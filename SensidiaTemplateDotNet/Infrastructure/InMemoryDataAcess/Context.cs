using SensidiaTemplateDotNet.Domain.Cars;
using System.Collections.ObjectModel;

namespace SensidiaTemplateDotNet.Infrastructure.InMemoryDataAcess
{
    public class Context
    {
        public Collection<Entities.Car> Cars { get; set; }
        public Collection<Entities.PickUp> Pickups { get; set; }

        public Context()
        {
            Cars = new Collection<Entities.Car>();
            Pickups = new Collection<Entities.PickUp>();
        }
    }
}
