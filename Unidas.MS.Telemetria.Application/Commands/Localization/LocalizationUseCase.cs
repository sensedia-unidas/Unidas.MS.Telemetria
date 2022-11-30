using Microsoft.Extensions.DependencyInjection;
using Unidas.MS.Telemetria.Application.Exceptions;
using Unidas.MS.Telemetria.Application.Interfaces.Commands.Localization;
using Unidas.MS.Telemetria.Application.Interfaces.Services.Localization.Source;
using Unidas.MS.Telemetria.Application.Interfaces.Services.MiX;
using Unidas.MS.Telemetria.Application.Services;
using Unidas.MS.Telemetria.Application.Services.Localization.MiX;
using Unidas.MS.Telemetria.Application.ViewModels.Localization;

namespace Unidas.MS.Telemetria.Application.Commands.Localization
{
    public class LocalizationUseCase : ILocalizationUseCase
    {
        private readonly IClientMiX _clientMiX;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public LocalizationUseCase(IClientMiX clientMiX, IServiceScopeFactory serviceScopeFactory)
        {
            _clientMiX = clientMiX;
            _serviceScopeFactory = serviceScopeFactory;
        }
        public async Task<LocalizationVM> Execute(string sinceDate, int sourceId, int quantity, string organizationIds)
        {
            List<long> listOrganizationIds = null;
            if (!String.IsNullOrEmpty(organizationIds))
                listOrganizationIds = organizationIds.Split(",").Select(x => long.Parse(x)).ToList();

            var source = this.Source(sourceId);

            var localizations = new List<LocalizationResultsVM>();

            if (listOrganizationIds == null || listOrganizationIds.Count == 0)
                localizations.Add(await source.GetAsync(sinceDate, quantity));
            else
            {


                List<Task> taskList = new List<Task>();

                listOrganizationIds.ForEach(organziationId =>
                {
                    taskList.Add(Task.Run(async () =>
                    {
                        LocalizationResultsVM localizationResult = await source.GetAsync(sinceDate, quantity, organziationId);

                        if (localizationResult != null)
                            localizations.Add(localizationResult);
                    }));

                });

                Task.WaitAll(taskList.ToArray());

            }

            var localizationVM = new LocalizationVM()
            {
                Results = localizations,
                SourceId = sourceId
            };

            return await Task.FromResult(localizationVM);

        }


        private ILocalizationSource Source(int id)
        {
            if (id == (int)SourceEnum.MiX)
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var localizationMiX = scope.ServiceProvider.GetService<MiXLocalization>();
                    return localizationMiX;
                }

            }
            throw new SourceIdNotFoundException();
        }
    }
}
