using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.ViewModels.HistoricalEvent;
using Unidas.MS.Telemetria.Application.ViewModels.Localization;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.Localization
{
    public interface ILocalizationUseCase
    {
        Task<LocalizationVM> Execute(string sinceDate, int sourceId, int quantity, string organizationGroupIds);
    }
}
