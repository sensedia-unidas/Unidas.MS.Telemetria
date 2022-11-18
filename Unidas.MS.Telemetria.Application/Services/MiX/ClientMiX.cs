using MiX.Integrate.API.Client;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;

namespace Unidas.MS.Telemetria.Application.Services.MiX
{
    public sealed class ClientMiX : IClientMiX
    {

        private string _baseUrl
        {
            get
            {
                return Config.GetFromAppSettings("MiX:ApiUrl");
            }
        }

        private EventsClient _events { get; set; }
        public EventsClient Events
        {
            get
            {
                if (_events != null)
                    return _events;

                _events = new EventsClient(_baseUrl, this.OwnerClientSettings());

                return _events;

            }
        }


        private DriversClient _drivers { get; set; }
        public DriversClient Drivers
        {
            get
            {
                if (_drivers != null)
                    return _drivers;

                _drivers = new DriversClient(_baseUrl, this.OwnerClientSettings());

                return _drivers;

            }
        }


        private DriverLicenceClient _driverLicence { get; set; }
        public DriverLicenceClient DriverLicence
        {
            get
            {
                if (_driverLicence != null)
                    return _driverLicence;

                _driverLicence = new DriverLicenceClient(_baseUrl, this.OwnerClientSettings());

                return _driverLicence;

            }
        }


        private LibraryEventsClient _libraryEvents { get; set; }
        public LibraryEventsClient LibraryEvents
        {
            get
            {
                if (_libraryEvents != null)
                    return _libraryEvents;

                _libraryEvents = new LibraryEventsClient(_baseUrl, this.OwnerClientSettings());

                return _libraryEvents;

            }
        }



        private LocationsClient _locations { get; set; }
        public LocationsClient Locations
        {
            get
            {
                if (_locations != null)
                    return _locations;

                _locations = new LocationsClient(_baseUrl, this.OwnerClientSettings());

                return _locations;

            }
        }


        private PositionsClient _positions { get; set; }
        public PositionsClient Positions
        {
            get
            {
                if (_positions != null)
                    return _positions;

                _positions = new PositionsClient(_baseUrl, this.OwnerClientSettings());

                return _positions;

            }
        }



        private TripsClient _trips { get; set; }
        public TripsClient Trips
        {
            get
            {
                if (_trips != null)
                    return _trips;

                _trips = new TripsClient(_baseUrl, this.OwnerClientSettings());

                return _trips;

            }
        }


        private AssetsClient _assets { get; set; }
        public AssetsClient Assets
        {
            get
            {
                if (_assets != null)
                    return _assets;

                _assets = new AssetsClient(_baseUrl, this.OwnerClientSettings());

                return _assets;

            }
        }




        private IdServerResourceOwnerClientSettings OwnerClientSettings()
        {

            return new IdServerResourceOwnerClientSettings()
            {
                BaseAddress = Config.GetFromAppSettings("MiX:IdentityServerBaseAddress"),
                ClientId = Config.GetFromAppSettings("MiX:IdentityServerClientId"),
                ClientSecret = Config.GetFromAppSettings("MiX:IdentityServerClientSecret"),
                UserName = Config.GetFromAppSettings("MiX:IdentityServerUserName"),
                Password = Config.GetFromAppSettings("MiX:IdentityServerPassword"),
                Scopes = Config.GetFromAppSettings("MiX:IdentityServerScopes")
            };
        }
    }
}
