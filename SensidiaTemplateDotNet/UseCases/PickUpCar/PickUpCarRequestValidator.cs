using FluentValidation;
using System;

namespace SensidiaTemplateDotNet.UseCases.PickUpCar
{
    public class PickUpCarRequestValidator : AbstractValidator<PickUpCarRequest>
    {
        public PickUpCarRequestValidator()
        {
            RuleFor(m => m.CarId).NotEmpty();
            RuleFor(m => m.RentedBy).NotEmpty();
        }
    }
}