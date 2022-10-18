namespace SensidiaTemplateDotNet.Domain.Cars
{
   
    public sealed class CarCannotBePickupExcepction : DomainException
    {
        internal CarCannotBePickupExcepction(string message)
            : base(message)
        { }
    }
}
