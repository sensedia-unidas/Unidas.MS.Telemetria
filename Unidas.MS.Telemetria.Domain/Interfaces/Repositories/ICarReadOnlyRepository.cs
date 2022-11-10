using Unidas.MS.Telemetria.Domain.Models.Cars;

namespace Unidas.MS.Telemetria.Domain.Interfaces.Repositories
{

    public interface ICarReadOnlyRepository
    {
        Task<Cars> Get(Guid id);
    }
}
