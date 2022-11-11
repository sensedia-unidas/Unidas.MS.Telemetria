using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidas.MS.Telemetria.Application.ViewModels.Vehicle
{

    public sealed class VehicleVM
    {
        public int SourceId { get; set; }

        public List<VehicleResultsVM> Results { get; set; }
    }

    public sealed class VehicleResultsVM
    {
        public long OrganizationId { get; set; }
        public object Result { get; set; }
        public bool HasMoreResult { get; set; }
    }
}
