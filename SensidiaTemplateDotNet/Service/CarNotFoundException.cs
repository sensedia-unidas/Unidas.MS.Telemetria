namespace SensidiaTemplateDotNet.Service
{
    internal sealed class CarNotFoundException : ApplicationException
    {
        internal CarNotFoundException(string message)
            : base(message)
        { }
    }
}
