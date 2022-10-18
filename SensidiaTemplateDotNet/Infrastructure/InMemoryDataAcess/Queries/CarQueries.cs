using SensidiaTemplateDotNet.Domain.Cars;
using SensidiaTemplateDotNet.Service.Queries;
using SensidiaTemplateDotNet.Service.Results;

namespace SensidiaTemplateDotNet.Infrastructure.InMemoryDataAcess.Queries
{
    public class CarQueries : ICarQueries
    {

        private readonly Context _context;
        public CarQueries(Context context)
        {
            _context = context;
        }
        public async Task<CarResult> GetCar(Guid carId)
        {

            Entities.Car carEntity = _context.Cars.FirstOrDefault(x => x.Id == carId);

            List<ICarTransaction> carTransactions = new List<ICarTransaction>();

            List<Entities.PickUp> pickups = _context.Pickups.Where(x => x.CarId == carId).ToList();

            foreach (Entities.PickUp transactionData in carTransactions)
            {
                PickUpCar pickup = PickUpCar.Load(transactionData.Id, transactionData.CarId, transactionData.RentedBy, transactionData.Latitude, transactionData.Longitude, transactionData.TransactionDate);

                carTransactions.Add(pickup);
            }


            var orderedTransactions = carTransactions.OrderBy(o => o.TransactionDate).ToList();

            CarTransactionCollection transactionCollection = new CarTransactionCollection();
            transactionCollection.Add(orderedTransactions);

            Car car = Car.Load(carEntity.Id, carEntity.Description, carEntity.Plate, transactionCollection);


            CarResult result = new CarResult(car);

            return await Task.FromResult<CarResult>(result);
        }
    }
}
