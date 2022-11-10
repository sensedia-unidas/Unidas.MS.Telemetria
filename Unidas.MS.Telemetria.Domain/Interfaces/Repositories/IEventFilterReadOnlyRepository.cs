using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Domain.Models.Cars;
using Unidas.MS.Telemetria.Domain.Models.EventFilter;

namespace Unidas.MS.Telemetria.Domain.Interfaces.Repositories
{
    public interface IEventFilterReadOnlyRepository
    {
        Task<IEnumerable<long>> GetAll();
    }
}
