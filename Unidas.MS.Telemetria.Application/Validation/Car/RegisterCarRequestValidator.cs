using FluentValidation;
using Unidas.MS.Telemetria.Application.ViewModels.Car;
using Unidas.MS.Telemetria.Application.ViewModels.Car.Requests;

namespace Unidas.MS.Telemetria.Application.Validation.Car
{
    public class RegisterCarRequestValidator : AbstractValidator<RegisterCarRequest>
    {
        public RegisterCarRequestValidator()
        {
            RuleFor(m => m.Plate).NotEmpty();
            RuleFor(m => m.Description).NotEmpty();
        }
    }
}
