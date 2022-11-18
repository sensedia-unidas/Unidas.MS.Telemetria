namespace Unidas.MS.Telemetria.Application.ViewModels.HistoricalEvent
{
    public sealed class HistoricalEventVM
    {
        public int SourceId { get; set; }

        public List<HistoricalEventResultsVM> Results { get; set; }
    }

    public sealed class HistoricalEventResultsVM
    {
        public bool? HasMoreResult { get; set; }
        public long OrganizationId { get; set; }
        public object Result { get; set; }
    }
}
