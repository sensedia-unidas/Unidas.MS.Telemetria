namespace SensidiaTemplateDotNet.Domain.Cars
{
    public sealed class CarWithoutTransactionException : DomainException
    {
        internal CarWithoutTransactionException(string message)
            : base(message)
        { }
    }
}