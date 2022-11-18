using Unidas.MS.Telemetria.Domain.Interfaces.Repositories;

namespace Unidas.MS.Telemetria.Infra.Repositories
{
    public class EventFilterRepository : IEventFilterReadOnlyRepository
    {

        private readonly TelemetriaContext _context;

        public EventFilterRepository(TelemetriaContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<long>> GetAll()
        {



            var query = _context.Events
              .Where(x => x.Active)
              .Select(x => x.Value);

            return query;


        }
    }
}
