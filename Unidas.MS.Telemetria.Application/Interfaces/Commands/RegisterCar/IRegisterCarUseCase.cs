using Unidas.MS.Telemetria.Application.ViewModels.Car.Results;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.RegisterCar
{
    public interface IRegisterCarUseCase
    {
        Task<RegisterCarResult> Execute(string description, string plate);
    }
}
