namespace SensidiaTemplateDotNet.Infrastructure
{
    internal sealed class CarNotFoundException : InfrastructureException
    {
        internal CarNotFoundException(string message)
            : base(message)
        { }
    }
}
