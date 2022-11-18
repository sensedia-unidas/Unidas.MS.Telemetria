using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidas.MS.Telemetria.Application.ViewModels.Localization
{


    public sealed class LocalizationVM
    {
        public int SourceId { get; set; }

        public List<LocalizationResultsVM> Results { get; set; }
    }

    public sealed class LocalizationResultsVM
    {
        public bool? HasMoreResult { get; set; }
        public long OrganizationId { get; set; }
        public object ResultPositions { get; set; }
        public object ResultLocations { get; set; }

    }

}