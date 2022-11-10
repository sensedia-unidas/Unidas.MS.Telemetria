using Unidas.MS.Telemetria.Domain.Models.Cars;

namespace Unidas.MS.Telemetria.Application.ViewModels.Car.Results
{
    public class RegisterCarResult
    {
        public CarResult Car { get; }

        public RegisterCarResult(Cars car)
        {
            Car = new CarResult(car);

        }
    }
}
