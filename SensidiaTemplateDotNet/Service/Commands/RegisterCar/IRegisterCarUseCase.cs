namespace SensidiaTemplateDotNet.Service.Commands.RegisterCar
{
    public interface IRegisterCarUseCase
    {
        Task<RegisterCarResult> Execute(string description, string plate);
    }
}
