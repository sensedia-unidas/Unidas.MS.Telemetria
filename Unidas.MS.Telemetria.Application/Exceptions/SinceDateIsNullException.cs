namespace Unidas.MS.Telemetria.Application.Exceptions
{
    internal sealed class SinceDateIsNullException : ApplicationException
    {
        internal SinceDateIsNullException()
            : base("A data de ínicio deve ser informada")
        { }
    }
}
