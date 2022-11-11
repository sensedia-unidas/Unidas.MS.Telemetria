using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidas.MS.Telemetria.Application.ViewModels.Driver
{
   
    public sealed class DriverVM
    {
        public int SourceId { get; set; }

        public List<DriverResultsVM> Results { get; set; }
    }

    public sealed class DriverResultsVM
    {
        public long OrganizationId { get; set; }
        public object Result { get; set; }
    }
}
