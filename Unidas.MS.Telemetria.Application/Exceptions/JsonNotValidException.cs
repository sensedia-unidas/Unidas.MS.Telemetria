using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Services;

namespace Unidas.MS.Telemetria.Application.Exceptions
{
    internal sealed class JsonNotValidException : ApplicationException
    {
        internal JsonNotValidException()
            : base($"O JSON informado não é um JSON valido.")
        { }
    }
}
