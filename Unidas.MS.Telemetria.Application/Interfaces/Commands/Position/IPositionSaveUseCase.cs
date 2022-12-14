using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unidas.MS.Telemetria.Application.ViewModels.Localization;
using Unidas.MS.Telemetria.Application.ViewModels.Position;

namespace Unidas.MS.Telemetria.Application.Interfaces.Commands.Position
{
    public interface IPositionSaveUseCase
    {
        Task Execute(int idSource, GolSatPositionsVM positions);
    }
}
