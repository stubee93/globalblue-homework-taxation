namespace Taxation.Infrastructure.Options;

public sealed class VatOptions
{
    public string CountryCode { get; init; } = string.Empty;
    public List<int> Rates { get; init; } = new();
}