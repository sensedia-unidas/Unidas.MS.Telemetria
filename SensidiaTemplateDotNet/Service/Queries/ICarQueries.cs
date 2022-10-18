using SensidiaTemplateDotNet.Service.Results;

namespace SensidiaTemplateDotNet.Service.Queries
{
    public interface ICarQueries
    {
        Task<CarResult> GetCar(Guid carId);
    }
}
