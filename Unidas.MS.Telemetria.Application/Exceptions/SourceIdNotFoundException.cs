using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unidas.MS.Telemetria.Application.Exceptions
{
    internal sealed class SourceIdNotFoundException : ApplicationException
    {
        internal SourceIdNotFoundException()
            : base("O SourceId não é valido.")
        { }
    }
}
