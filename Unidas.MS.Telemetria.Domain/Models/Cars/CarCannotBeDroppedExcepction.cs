﻿namespace Unidas.MS.Telemetria.Domain.Models.Cars
{
    public sealed class CarCannotBeDroppedExcepction : DomainException
    {
        internal CarCannotBeDroppedExcepction(string message)
            : base(message)
        { }
    }
}