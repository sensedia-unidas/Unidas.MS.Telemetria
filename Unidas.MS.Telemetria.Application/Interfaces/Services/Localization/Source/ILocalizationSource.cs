using Unidas.MS.Telemetria.Application.ViewModels.Localization;

namespace Unidas.MS.Telemetria.Application.Interfaces.Services.Localization.Source
{
    public interface ILocalizationSource
    {
        Task<LocalizationResultsVM> GetAsync(string sinceDate, int quantity, long? organizationId = null);
    }
}
