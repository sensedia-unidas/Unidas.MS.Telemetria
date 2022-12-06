using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidas.MS.Telemetria.Application.ViewModels.Position
{
    

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class GolSatCan
    {
        public double comb { get; set; }
        public bool cinto { get; set; }
        public bool freio { get; set; }
        public bool limp { get; set; }
    }

    public class GolSatEvento
    {
        public string desc { get; set; }
        public bool src { get; set; }
    }

    public class GolSatInfo
    {
        public double odo { get; set; }
        public double odoTotal { get; set; }
        public int rpm { get; set; }
        public int vel { get; set; }
        public bool ign { get; set; }
        public bool log { get; set; }
        public bool gps { get; set; }
    }

    public class GolSatMacro
    {
        public string desc { get; set; }
        public string aprProc { get; set; }
    }

    public class GolSatItem
    {
        public int id { get; set; }
        public string placa { get; set; }
        public string serialNumber { get; set; }
        public List<double> coord { get; set; }
        public string end { get; set; }
        public DateTime dInc { get; set; }
        public DateTime dPos { get; set; }
        public string motorista { get; set; }
        public GolSatInfo info { get; set; }
        public List<GolSatEvento> eventos { get; set; }
        public List<GolSatMacro> macros { get; set; }
        public GolSatCan can { get; set; }
    }

    public class GolSatPositionsVM
    {
        public List<GolSatItem> positions { get; set; }
    }


}
