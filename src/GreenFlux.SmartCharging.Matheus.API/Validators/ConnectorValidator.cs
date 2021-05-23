using FluentValidation;
using GreenFlux.SmartCharging.Matheus.API.Resources;

namespace GreenFlux.SmartCharging.Matheus.API.Validators
{
    public class SaveConnectorValidator : AbstractValidator<SaveConnectorResource>
    {
        public SaveConnectorValidator()
        {
            RuleFor(c => c.MaxCurrentAmp).NotEmpty().WithMessage("'Max Current Amp' field is required");
            RuleFor(c => c.MaxCurrentAmp).LessThanOrEqualTo(float.MaxValue).WithMessage("'Max Current Amp' is required to be less than " + float.MaxValue.ToString());
            RuleFor(c => c.MaxCurrentAmp).GreaterThan(0).WithMessage("'Max Current Amp' is required to be greater than 0");
        }
    }

    public class PatchConnectorValidator : AbstractValidator<PatchConnectorResource>
    {
        public PatchConnectorValidator()
        {
            RuleFor(c => c.MaxCurrentAmp).LessThanOrEqualTo(float.MaxValue).WithMessage("'Max Current Amp' is required to be less than " + float.MaxValue.ToString()).When(c => c.MaxCurrentAmp.HasValue);
            RuleFor(c => c.MaxCurrentAmp).GreaterThan(0).WithMessage("'Max Current Amp' is required to be greater than 0").When(c => c.MaxCurrentAmp.HasValue);
        }
    }
}
