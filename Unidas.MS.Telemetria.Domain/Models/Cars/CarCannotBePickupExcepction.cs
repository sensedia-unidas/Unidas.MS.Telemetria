namespace Unidas.MS.Telemetria.Domain.Models.Cars
{
   
    public sealed class CarCannotBePickupExcepction : DomainException
    {
        internal CarCannotBePickupExcepction(string message)
            : base(message)
        { }
    }
}
