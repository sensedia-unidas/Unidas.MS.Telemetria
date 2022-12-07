using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidas.MS.Telemetria.Application.ViewModels.Event
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class EventDefaultDatum
    {
        public long? id { get; set; }
        public string? placa { get; set; }
        public string? rastreadorNumeroSerie { get; set; }
        public string? tipoEvento { get; set; }
        public double? tempo { get; set; }
        public int? rastreadorBateriaInternaAtiva { get; set; }
        public int? rastreadorBateriaInternaTensao { get; set; }
        public DateTime? dataHoraEvento { get; set; }
        public int? gpsFixado { get; set; }
        public double? latitudeInicio { get; set; }
        public double? longitudeInicio { get; set; }
        public double? latitudeFim { get; set; }
        public double? longitudeFim { get; set; }
        public double? velocidade { get; set; }
        public double? altitude { get; set; }
        public double? odometro { get; set; }
        public int? bateriaExternaAtiva { get; set; }
        public int? bateriaExternaTensao { get; set; }
        public int? ignicao { get; set; }
        public string? revGeoLogradouro { get; set; }
        public string? revGeoFaixaNumeracao { get; set; }
        public string? revGeoCep { get; set; }
        public string? revGeoMunicipio { get; set; }
        public string? revGeoUf { get; set; }
        public double? horimetro { get; set; }
        public int? sleep { get; set; }
        public double? rpm { get; set; }
    }

    public class EventDefaultVM
    {
        public string fornecedor { get; set; }
        public string tipoArquivo { get; set; }
        public List<EventDefaultDatum> data { get; set; }
    }


}
