using SensidiaTemplateDotNet.Domain.Cars;
using SensidiaTemplateDotNet.Service.Repositores;

namespace SensidiaTemplateDotNet.Service.Commands.RegisterCar
{
    public sealed  class RegisterUseCase : IRegisterCarUseCase
    {


        private readonly ICarWriteOnlyRepository carWriteOnlyRepository;

        public RegisterUseCase(ICarWriteOnlyRepository carWriteOnlyRepository)
        {
            this.carWriteOnlyRepository = carWriteOnlyRepository;
        }

        public async Task<RegisterCarResult> Execute(string description, string plate)
        {
            Car car = new Car(description, plate);

            await carWriteOnlyRepository.Add(car);

            RegisterCarResult result = new RegisterCarResult(car);

            return result;
        }
    }
}
