namespace Unidas.MS.Telemetria.Application.ViewModels.SubTrip
{


    public sealed class SubTripVM
    {
        public int SourceId { get; set; }

        public List<SubTripResultsVM> Results { get; set; }
    }

    public sealed class SubTripResultsVM
    {
        public long OrganizationId { get; set; }
        public object Result { get; set; }
        public bool HasMoreResult { get; set; }
    }
}
