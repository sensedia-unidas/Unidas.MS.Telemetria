namespace Unidas.MS.Telemetria.Domain.Interfaces.Repositories
{
    public interface IEventFilterReadOnlyRepository
    {
        Task<IEnumerable<long>> GetAll();
    }
}
