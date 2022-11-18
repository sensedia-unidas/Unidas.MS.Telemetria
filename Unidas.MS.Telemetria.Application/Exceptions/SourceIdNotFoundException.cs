namespace Unidas.MS.Telemetria.Application.Exceptions
{
    internal sealed class SourceIdNotFoundException : ApplicationException
    {
        internal SourceIdNotFoundException()
            : base("O SourceId não é valido.")
        { }
    }
}
