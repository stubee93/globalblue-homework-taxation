using FluentValidation;
using Taxation.Application.Abstractions;

namespace Taxation.Application.UseCases.CalculateVat;

public class CalculateVatCommandValidator : AbstractValidator<CalculateVatCommand>
{
    public CalculateVatCommandValidator(IVatRateCatalog catalog)
    {
        RuleFor(x => new[] { x.Net, x.Gross, x.Vat }.Count(v => v.HasValue))
            .Equal(1)
            .WithMessage("Exactly one of Net, Gross, or Vat must be provided.");

        When(
            x => x.Net.HasValue,
            () => RuleFor(x => x.Net!.Value).GreaterThan(0));
        When(
            x => x.Gross.HasValue,
            () => RuleFor(x => x.Gross!.Value).GreaterThan(0));
        When(
            x => x.Vat.HasValue,
            () => RuleFor(x => x.Vat!.Value).GreaterThan(0));

        RuleFor(x => x.VatRatePercent)
            .Must((cmd, r) => catalog.GetAllowedRates(cmd.CountryCode).Contains(r))
            .WithMessage(
                cmd =>
                    // TODO: We could refine the error message to prevent leaking information
                    $"VAT rate must be one of the allowed rates for country '{cmd.CountryCode}': {string.Join(", ", catalog.GetAllowedRates(cmd.CountryCode))}");
    }
}