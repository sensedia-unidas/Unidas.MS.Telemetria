namespace SensidiaTemplateDotNet.Service.Commands.PickUpCar
{
    using SensidiaTemplateDotNet.Domain.Cars;
    using SensidiaTemplateDotNet.Service.Repositores;
    using System.Threading.Tasks;

    public sealed class PickUpCarUseCase : IPickUpCarUseCase
    {

        private readonly ICarReadOnlyRepository carReadOnlyRepository;
        private readonly ICarWriteOnlyRepository carWriteOnlyRepository;

        public PickUpCarUseCase(ICarReadOnlyRepository carReadOnlyRepository, ICarWriteOnlyRepository carWriteOnlyRepository)
        {
            this.carReadOnlyRepository = carReadOnlyRepository;
            this.carWriteOnlyRepository = carWriteOnlyRepository;
        }

        public async Task<Guid> Execute(Guid carId, string rentedBy, long latitude, long longitude)
        {
            Car car = await carReadOnlyRepository.Get(carId);
            if (car == null)
                throw new Service.CarNotFoundException($"O carro {carId} não existe");

           

            var pickUp = car.Pickup(rentedBy, latitude, longitude);

            await this.carWriteOnlyRepository.Update(car, pickUp);

            return pickUp.Id;
        }
    }
}
