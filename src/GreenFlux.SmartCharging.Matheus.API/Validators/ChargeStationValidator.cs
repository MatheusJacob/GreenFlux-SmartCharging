using FluentValidation;
using GreenFlux.SmartCharging.Matheus.API.Resources;
using GreenFlux.SmartCharging.Matheus.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GreenFlux.SmartCharging.Matheus.API.Validators
{
    public class SaveChargeStationValidator : AbstractValidator<SaveChargeStationResource>
    {
        public SaveChargeStationValidator()
        {
            RuleFor(c => c.Name).NotEmpty().WithMessage("'Name' field is required");
            RuleFor(c => c.Connectors).NotNull().WithMessage("A charge station needs at least " + ChargeStation.MinConnectors.ToString() + " connector");
            RuleFor(c => c.Connectors.Count).LessThanOrEqualTo(ChargeStation.MaxConnectors).WithMessage("A charge station can't have more than " + ChargeStation.MaxConnectors.ToString() + " connectors");
            RuleFor(c => c.Connectors.Count).GreaterThanOrEqualTo(ChargeStation.MinConnectors).WithMessage("A charge station needs at least " + ChargeStation.MinConnectors.ToString() + " connector");
        }
    }

    public class PatchChargeStationValidator : AbstractValidator<PatchGroupResource>
    {
        public PatchChargeStationValidator()
        {            
            RuleFor(g => g.Capacity).GreaterThan(0).WithMessage("'Capacity' field must be greater than 0").When(g => g.Capacity.HasValue);
            RuleFor(g => g.Capacity).LessThan(float.MaxValue).WithMessage($"'Capacity' field must be less than {float.MaxValue.ToString()}").When(g => g.Capacity.HasValue);
        }
    }
}
