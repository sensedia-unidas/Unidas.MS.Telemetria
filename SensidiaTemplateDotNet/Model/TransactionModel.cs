using System.Security.Cryptography.X509Certificates;

namespace SensidiaTemplateDotNet.Model
{
    public sealed class TransactionModel
    {
        public DateTime TransactionDate { get; set; }
        public string RentedBy { get; set; }
        public long Latitude { get; set; }
        public long Longitude { get; set; }
        public string Action { get; set; }

        public TransactionModel(string rentedBy, string action, long latitude, long longitude, DateTime transactionDate)
        {
            RentedBy = rentedBy;    
            Action = action;    
            TransactionDate = transactionDate;
            Latitude = latitude;
            Longitude = longitude;
        }
    }
}
