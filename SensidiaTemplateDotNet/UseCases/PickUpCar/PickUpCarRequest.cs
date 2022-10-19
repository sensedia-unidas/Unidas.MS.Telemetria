using System.ComponentModel.DataAnnotations;

namespace SensidiaTemplateDotNet.UseCases.PickUpCar
{
    public sealed class PickUpCarRequest
    {
        
        public Guid CarId { get; set; }
        public string RentedBy { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
    }
}
