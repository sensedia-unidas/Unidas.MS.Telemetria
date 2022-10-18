using SensidiaTemplateDotNet.Domain.Cars;
using SensidiaTemplateDotNet.Service.Repositores;
using System.Security.Principal;
using System.Transactions;

namespace SensidiaTemplateDotNet.Infrastructure.InMemoryDataAcess.Repositories
{
    public class CarRepository : ICarReadOnlyRepository, ICarWriteOnlyRepository
    {
        private readonly Context _context;

        public CarRepository(Context context)
        {
            _context = context;
        }
        public async Task Add(Car car)
        {

            Entities.Car carEntity = new Entities.Car()
            {
               Description = car.Description,
               Id = car.Id,
               Plate = car.Plate
            };

            _context.Cars.Add(carEntity);
            await Task.CompletedTask;
        }

        public async Task Delete(Car car)
        {
            Entities.Car? carOld = _context.Cars.SingleOrDefault(e => e.Id == car.Id);

            if (carOld != null)
                _ = _context.Cars.Remove(carOld);

            await Task.CompletedTask;
        }

        public async Task<Car> Get(Guid id)
        {
            Entities.Car? carEntity = _context.Cars.SingleOrDefault(e => e.Id == id);

            List<ICarTransaction> carTransactions = new List<ICarTransaction>();

            List<Entities.PickUp> pickups = _context.Pickups.Where(x => x.CarId == id).ToList();

            foreach(Entities.PickUp transactionData in carTransactions)
            {
                PickUpCar pickup = PickUpCar.Load(transactionData.Id, transactionData.CarId, transactionData.RentedBy, transactionData.Latitude, transactionData.Longitude, transactionData.TransactionDate);

                carTransactions.Add(pickup);
            }


            var orderedTransactions = carTransactions.OrderBy(o => o.TransactionDate).ToList();

            CarTransactionCollection transactionCollection = new CarTransactionCollection();
            transactionCollection.Add(orderedTransactions);

            Car car = Car.Load(carEntity.Id, carEntity.Description, carEntity.Plate, transactionCollection);

            return await Task.FromResult<Car>(car);
        }

        public async Task Update(Car car, PickUpCar pickUp)
        {
            Entities.PickUp pickupEntity = new Entities.PickUp()
            {
                Id = pickUp.Id,
                CarId = car.Id,
                Latitude = pickUp.Latitude,
                Longitude = pickUp.Longitude,
                RentedBy = pickUp.RentedBy,
                TransactionDate = pickUp.TransactionDate
            };

            _context.Pickups.Add(pickupEntity);

            await Task.CompletedTask;

        }
    }
}
