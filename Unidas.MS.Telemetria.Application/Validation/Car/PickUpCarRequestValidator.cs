using FluentValidation;
using Unidas.MS.Telemetria.Application.ViewModels.Car;
using Unidas.MS.Telemetria.Application.ViewModels.Car.Requests;

namespace Unidas.MS.Telemetria.Application.Validation.Car
{
    public class PickUpCarRequestValidator : AbstractValidator<PickupCarRequest>
    {
        public PickUpCarRequestValidator()
        {
            RuleFor(m => m.CarId).NotEmpty();
            RuleFor(m => m.RentedBy).NotEmpty();
        }
    }
}
