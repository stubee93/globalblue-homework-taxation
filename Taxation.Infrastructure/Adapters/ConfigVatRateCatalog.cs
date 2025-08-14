using Microsoft.Extensions.Options;
using Taxation.Application.Abstractions;
using Taxation.Domain.Exceptions;
using Taxation.Infrastructure.Options;

namespace Taxation.Infrastructure.Adapters;

public sealed class ConfigVatRateCatalog(IReadOnlyCollection<VatOptions> vatOptions) : IVatRateCatalog
{
    public IReadOnlyCollection<int> GetAllowedRates(string countryCode)
    {
        return vatOptions
            .FirstOrDefault(x => x.CountryCode.Equals(countryCode, StringComparison.OrdinalIgnoreCase))?.Rates
            ?? throw new DomainException($"Could not find rates for country with code {countryCode}.");
    }
}