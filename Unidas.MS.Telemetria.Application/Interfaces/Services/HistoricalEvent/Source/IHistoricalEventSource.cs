﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.ViewModels.Historical;

namespace Unidas.MS.Telemetria.Application.Interfaces.Services.HistoricalEvent.Source
{
    public interface IHistoricalEventSource
    {
        Task<HistoricalEventResultsVM> Get(string sinceDate, int quantity,  string? organizationId = null);
    }
}
