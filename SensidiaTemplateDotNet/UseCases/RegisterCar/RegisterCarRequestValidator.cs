using FluentValidation;
using SensidiaTemplateDotNet.UseCases.PickUpCar;

namespace SensidiaTemplateDotNet.UseCases.RegisterCar
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