using SensidiaTemplateDotNet.Domain.Cars;
using SensidiaTemplateDotNet.Service.Results;

namespace SensidiaTemplateDotNet.Service.Commands.RegisterCar
{
    public class RegisterCarResult
    {
        public CarResult Car { get; }

        public RegisterCarResult(Car car)
        {
            Car = new CarResult(car);

        }
    }
}
