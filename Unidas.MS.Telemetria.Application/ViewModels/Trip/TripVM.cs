namespace Unidas.MS.Telemetria.Application.ViewModels.Trip
{


    public sealed class TripVM
    {
        public int SourceId { get; set; }

        public List<TripResultsVM> Results { get; set; }
    }

    public sealed class TripResultsVM
    {
        public long OrganizationId { get; set; }
        public object Result { get; set; }
        public bool HasMoreResult { get; set; }
    }
}
