namespace SensidiaTemplateDotNet.Infrastructure.InMemoryDataAcess.Entities
{
    public class PickUp
    {
        public Guid Id { get; set; }
        public Guid CarId { get; set; }

        public string RentedBy { get; set; }

        public long Latitude { get; set; }
        public long Longitude { get; set; }

        public DateTime TransactionDate { get; set; }
    }
}
