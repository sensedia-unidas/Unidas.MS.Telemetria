using MiX.Integrate.API.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidas.MS.Telemetria.Application.Interfaces.Services.MiX
{
    public interface IClientMiX
    {
        EventsClient Events { get; }
        DriversClient Drivers { get; }
        DriverLicenceClient DriverLicence { get; }
        LibraryEventsClient LibraryEvents { get; }
        LocationsClient Locations { get; }
        PositionsClient Positions { get; }

        TripsClient Trips { get; }
        AssetsClient Assets { get; }

    }
} 
