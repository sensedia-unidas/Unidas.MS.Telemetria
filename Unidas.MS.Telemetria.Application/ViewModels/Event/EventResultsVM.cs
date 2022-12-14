namespace Unidas.MS.Telemetria.Application.ViewModels.Event
{



    public sealed class EventVM
    {
        public int SourceId { get; set; }

        public List<EventResultsVM> Results { get; set; }
    }

    public sealed class EventResultsVM
    {
        public long OrganizationId { get; set; }
        public object Result { get; set; }
    }
}
