using System.ComponentModel.Design.Serialization;
using System.Transactions;

namespace SensidiaTemplateDotNet.Domain.Cars
{
    public sealed class Car : IEntity, IAggregateRoot
    {
        public Guid Id { get; private set; }

        public string Description { get; set; }

       

        public string Plate { get; set; }


        public IReadOnlyCollection<ICarTransaction> GetTransactions()
        {
            IReadOnlyCollection<ICarTransaction> readOnly = _transactions.GetTransactions();
            return readOnly;
        }

        private CarTransactionCollection _transactions;

        public Car(string description, string plate)
        {
            Id = Guid.NewGuid();
            Description = description;
            Plate = plate;
            _transactions = new CarTransactionCollection();
        }

        public void Pickup(string rentedBy, long latitude, long longitude)
        {
            if (_transactions.GetLastTransactionType() == "PickUp")
                throw new CarCannotBePickupExcepction($"O carro {Id} já está alugado!");

            var pickUp = new PickUpCar(Id, rentedBy, latitude, longitude);

            _transactions.Add(pickUp); 
        }

        public (long latitude, long longitude) GetLastPosition()
        {
            var lastTransaction = _transactions.GetLastTransaction();

            if (lastTransaction == null)
                throw new CarWithoutTransactionException($"O carro {Id} não possúi transação");

            return (lastTransaction.Latitude, lastTransaction.Longitude);
        }

        private Car() { }

        public static Car Load(Guid id, string description, string Plate, CarTransactionCollection transactions)
        {
            Car car = new Car();
            car.Id = id;
            car.Description = description;
            car.Plate = Plate;
            car._transactions = transactions;
            return car;

        }
    }
}
