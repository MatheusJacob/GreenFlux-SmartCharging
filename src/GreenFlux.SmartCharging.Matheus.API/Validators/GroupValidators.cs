using FluentValidation;
using GreenFlux.SmartCharging.Matheus.API.Resources;

namespace GreenFlux.SmartCharging.Matheus.API.Validators
{
    public class SaveGroupValidator : AbstractValidator<SaveGroupResource>
    {
        public SaveGroupValidator()
        {
            RuleFor(g => g.Name).NotEmpty().WithMessage("'Name' field is required");
            RuleFor(g => g.Capacity).NotEmpty().WithMessage("'Capacity' field is required");
            RuleFor(g => g.Capacity).GreaterThan(0).WithMessage("'Capacity' field must be greater than 0");
            RuleFor(g => g.Capacity).LessThan(float.MaxValue).WithMessage($"'Capacity' field must be less than {float.MaxValue.ToString()}");
        }
    }

    public class PatchGroupValidator : AbstractValidator<PatchGroupResource>
    {
        public PatchGroupValidator()
        {
            RuleFor(g => g.Capacity).GreaterThan(0).WithMessage("'Capacity' field must be greater than 0").When(g => g.Capacity.HasValue);
            RuleFor(g => g.Capacity).LessThan(float.MaxValue).WithMessage($"'Capacity' field must be less than {float.MaxValue.ToString()}").When(g => g.Capacity.HasValue);
        }
    }
}
