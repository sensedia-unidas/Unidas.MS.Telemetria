﻿namespace Unidas.MS.Telemetria.Domain.Models
{
    public class DomainException : Exception
    {
        internal DomainException(string businessMessage)
            : base(businessMessage)
        {

          

        }
    }
}
