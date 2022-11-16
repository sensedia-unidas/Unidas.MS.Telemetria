using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidas.MS.Telemetria.Application.ViewModels
{
    public class AppSettings
    {
        public ApplicationSettings AXConnectorSettings { get; set; } = new ApplicationSettings();
    }

    public class ApplicationSettings
    {
        public MiX MiX { get; set; }
    }

    public class MiX
    {
        public string ApiUrl { get; set; }
        public string IdentityServerBaseAddress { get; set; }
        public string IdentityServerClientId { get; set; }
        public string IdentityServerClientSecret { get; set; }
        public string IdentityServerUserName { get; set; }
        public string IdentityServerPassword { get; set; }
        public string IdentityServerScopes { get; set; }
        public string ClientSettingsProviderServiceUri { get; set; }
    }
}
