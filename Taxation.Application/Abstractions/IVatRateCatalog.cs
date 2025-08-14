namespace Taxation.Application.Abstractions;

public interface IVatRateCatalog
{
    IReadOnlyCollection<int> GetAllowedRates(string countryCode);
}