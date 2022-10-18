namespace SensidiaTemplateDotNet.Model
{
    public sealed class CarDetailModel
    {
        public Guid CarId { get; }
        
        public string Description { get; set; }
        public string Plate { get; set; }
        public List<TransactionModel> Transactions { get; }

        public CarDetailModel(Guid carId, string description, string plate,  List<TransactionModel> transactions)
        {
            CarId = carId;
            Description = description;
            Plate = plate;
            Transactions = transactions;
        }
    }
}
