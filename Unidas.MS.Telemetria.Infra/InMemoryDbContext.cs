using System.Collections.ObjectModel;
using Unidas.MS.Telemetria.Domain.Models.Cars;

namespace Unidas.MS.Telemetria.Infra
{
    public class InMemoryDbContext
    {
        public Collection<Cars> Cars { get; set; }
        public Collection<PickUpCar> Pickups { get; set; }

        public InMemoryDbContext()
        {
            Cars = new Collection<Cars>();
            Pickups = new Collection<PickUpCar>();
        }
    }
}
