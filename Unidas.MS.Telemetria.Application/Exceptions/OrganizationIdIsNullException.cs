using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.Services;

namespace Unidas.MS.Telemetria.Application.Exceptions
{
    internal sealed class OrganizationIdIsNullException : ApplicationException
    {
        internal OrganizationIdIsNullException(string service, SourceEnum source)
            : base($"É obrigatório informar o OrganizationId para o service: {service} / source: {source}")
        { }
    }
}
