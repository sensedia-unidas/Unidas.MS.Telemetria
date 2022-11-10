using Unidas.MS.Telemetria.Application.ViewModels;

namespace Unidas.MS.Telemetria.Application.Interfaces
{
    public interface IMinimalValidator
    {
        ValidationResult Validate<T>(T model);
    }
}
